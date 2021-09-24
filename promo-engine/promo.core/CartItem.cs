using System;
using System.Collections.Generic;
using System.Text;

namespace promo.core
{
    public class CartItem
    {
        public CartItem(Product product)
        {
            Product = product;
        }
        public string Id { get; set; } = string.Empty;
        public DateTime UtcCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UtcUpdatedAt { get; set; }
        public Product Product { get; }
        public decimal Price { get; set; }
        public int ProductCount { get; set; }
    }
}
