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
        void AddToCart(Product product);
        void RemoveFromCart(OrderItem item);
        void UpdateQuantity(OrderItem item, int quantity);
        void UpdateSize(OrderItem item, string size); // Add this line
    }

    public class CartService : ICartService
    {
        private readonly List<OrderItem> _cartItems = new();

        public event Action OnChange;

        public List<OrderItem> GetCartItems() => _cartItems;

        public void AddToCart(Product product)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                // Set the default size when adding to cart
                var defaultSize = product.Sizes?.Split(',').FirstOrDefault()?.Trim();
                _cartItems.Add(new OrderItem
                {
                    Product = product,
                    Quantity = 1,
                    Price = product.Price,
                    Size = defaultSize ?? string.Empty // Ensure a size is set
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
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.ProductId == item.Product.ProductId);
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

        // Add this new method
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