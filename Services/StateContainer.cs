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
            CartItems.Remove(item);
            await SaveState();
            NotifyStateChanged();
        }

        public async Task UpdateCartItemQuantity(OrderItem item, int quantity)
        {
            var itemInCart = CartItems.FirstOrDefault(i => i.Product.ProductId == item.Product.ProductId && i.Size == item.Size && i.CustomName == item.CustomName && i.CustomNumber == item.CustomNumber);
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
                CartItems = JsonSerializer.Deserialize<List<OrderItem>>(cartJson) ?? new List<OrderItem>();
            }
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}