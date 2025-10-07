using MaillotStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaillotStore.Services
{
    public interface ICartService
    {
        event Action OnChange;
        List<OrderItem> GetCartItems();
        // --- Updated method signature to include all customization options ---
        void AddToCart(Product product, int quantity, string size, string? customName, int? customNumber);
        void RemoveFromCart(OrderItem item);
        void UpdateQuantity(OrderItem item, int quantity);
        void UpdateSize(OrderItem item, string size);

       
    }

    public class CartService : ICartService
    {
        private readonly List<OrderItem> _cartItems = new();

        public event Action OnChange;

        public List<OrderItem> GetCartItems() => _cartItems;

        // --- Updated AddToCart implementation ---
        public void AddToCart(Product product, int quantity, string size, string? customName, int? customNumber)
        {
            // Find an item with the same product ID, size, and customization
            var existingItem = _cartItems.FirstOrDefault(i =>
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
                _cartItems.Add(new OrderItem
                {
                    Product = product,
                    Quantity = quantity,
                    Price = product.Price,
                    Size = size,
                    CustomName = customName,
                    CustomNumber = customNumber
                });
            }
            NotifyStateChanged();
        }

        public void RemoveFromCart(OrderItem item)
        {
            _cartItems.Remove(item);
            NotifyStateChanged();
        }

        public void UpdateQuantity(OrderItem item, int quantity)
        {
            // Use a more reliable way to find the item, like a unique ID if available,
            // but for now, matching on product and customization is robust.
            var existingItem = _cartItems.FirstOrDefault(i =>
                i.Product.ProductId == item.Product.ProductId &&
                i.Size == item.Size &&
                i.CustomName == item.CustomName &&
                i.CustomNumber == item.CustomNumber);

            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
                if (existingItem.Quantity <= 0)
                {
                    _cartItems.Remove(existingItem);
                }
            }
            NotifyStateChanged();
        }

        public void UpdateSize(OrderItem item, string size)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.ProductId == item.Product.ProductId);
            if (existingItem != null)
            {
                existingItem.Size = size;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}