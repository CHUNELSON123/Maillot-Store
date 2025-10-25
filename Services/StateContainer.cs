using MaillotStore.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaillotStore.Services
{
    public class StateContainer
    {
        private readonly IJSRuntime _jsRuntime;

        public StateContainer(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public List<OrderItem> CartItems { get; private set; } = new();

        public event Action OnChange;

        public async Task AddToCart(Product product, int quantity, string size, string customName, int? customNumber)
        {
            var existingItem = CartItems.FirstOrDefault(i =>
                i.Product.ProductId == product.ProductId &&
                i.Size == size &&
                i.CustomName == customName &&
                i.CustomNumber == customNumber);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                CartItems.Add(new OrderItem
                {
                    Product = product,
                    Quantity = quantity,
                    Price = product.Price,
                    Size = size,
                    CustomName = customName,
                    CustomNumber = customNumber
                });
            }

            await SaveState();
            NotifyStateChanged();
        }

        public async Task RemoveFromCart(OrderItem item)
        {
            // Find the item based on ProductId, Size, CustomName, and CustomNumber
            var itemToRemove = CartItems.FirstOrDefault(i =>
                i.Product.ProductId == item.Product.ProductId &&
                i.Size == item.Size &&
                i.CustomName == item.CustomName &&
                i.CustomNumber == item.CustomNumber);

            if (itemToRemove != null)
            {
                CartItems.Remove(itemToRemove);
                await SaveState();
                NotifyStateChanged();
            }
        }


        public async Task UpdateCartItemQuantity(OrderItem item, int quantity)
        {
            var itemInCart = CartItems.FirstOrDefault(i =>
                i.Product.ProductId == item.Product.ProductId &&
                i.Size == item.Size &&
                i.CustomName == item.CustomName &&
                i.CustomNumber == item.CustomNumber);

            if (itemInCart != null)
            {
                itemInCart.Quantity = quantity;
                if (itemInCart.Quantity <= 0)
                {
                    CartItems.Remove(itemInCart);
                }
                await SaveState();
                NotifyStateChanged();
            }
        }

        // --- START: NEW METHOD ---
        public async Task UpdateCartItemSize(OrderItem item, string newSize)
        {
            // Find the item based on ProductId, *original* Size, CustomName, and CustomNumber
            var itemInCart = CartItems.FirstOrDefault(i =>
                i.Product.ProductId == item.Product.ProductId &&
                i.Size == item.Size && // Use the item's current size to find it
                i.CustomName == item.CustomName &&
                i.CustomNumber == item.CustomNumber);

            if (itemInCart != null)
            {
                // Check if an item with the *new* size already exists (for merging)
                var existingItemWithNewSize = CartItems.FirstOrDefault(i =>
                   i.Product.ProductId == item.Product.ProductId &&
                   i.Size == newSize && // Check for the new size
                   i.CustomName == item.CustomName &&
                   i.CustomNumber == item.CustomNumber &&
                   i != itemInCart); // Make sure it's not the same item we are changing

                if (existingItemWithNewSize != null)
                {
                    // Merge quantities: Add current item's quantity to the existing one
                    existingItemWithNewSize.Quantity += itemInCart.Quantity;
                    // Remove the original item we were changing
                    CartItems.Remove(itemInCart);
                }
                else
                {
                    // No existing item found with the new size, just update the size
                    itemInCart.Size = newSize;
                }

                await SaveState();
                NotifyStateChanged();
            }
        }
        // --- END: NEW METHOD ---

        public async Task ClearCart()
        {
            CartItems.Clear();
            await SaveState();
            NotifyStateChanged();
        }


        public async Task SaveState()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "maillot_store_cart", JsonSerializer.Serialize(CartItems));
        }

        public async Task LoadState()
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "maillot_store_cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                try
                {
                    CartItems = JsonSerializer.Deserialize<List<OrderItem>>(cartJson) ?? new List<OrderItem>();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing cart: {ex.Message}");
                    CartItems = new List<OrderItem>(); // Reset cart if deserialization fails
                    await SaveState(); // Clear the invalid state in local storage
                }
            }
            else
            {
                CartItems = new List<OrderItem>(); // Ensure CartItems is initialized if localStorage is empty
            }
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}