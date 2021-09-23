using System;
using System.Collections.Generic;
using System.Text;

namespace promo.core
{
    public class ProductKey
    {
        public ProductKey(string productId, string productType)
        {
            ProductId = productId;
            ProductType = productType;
        }

        public string ProductId { get; }
        public string ProductType { get; }
    }
}
