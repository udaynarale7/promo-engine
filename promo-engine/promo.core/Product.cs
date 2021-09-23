using System;
using System.Collections.Generic;
using System.Text;

namespace promo.core
{
    public class Product
    {
        public Product(ProductKey productKey)
        {
            ProductKey = productKey;
        }
        public ProductKey ProductKey { get; }
    }
}
