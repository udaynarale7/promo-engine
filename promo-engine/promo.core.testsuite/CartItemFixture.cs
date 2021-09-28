using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace promo.core.testsuite
{
    public class CartItemFixture
    {
        [Fact]
        public void Newly_created_cartItem_should_have_zero_price()
        {
            var cartItem = new CartItem(null);
            Assert.Equal(0, cartItem.Price);
        }

        private readonly string Currency = "USD";
        public CartItem ItemAInUSD
        {
            get
            {

                var productKey = new ProductKey("productA", "productBType");
                var product = new Product(productKey);
                return new CartItem(product) { Price = 10, ProductCount = 1 };
            }
        }

        public CartItem ItemBInUSD
        {
            get
            {
                var productKey = new ProductKey("productB", "productBType");
                var product = new Product(productKey);
                return new CartItem(product) { Price = 100, ProductCount = 1 };
            }
        }
        [Fact]
        public void Adding_new_item_to_cart_should_update_item_count()
        {
            var cart = new Cart(Currency);
            cart.Add(ItemAInUSD);
            cart.Add(ItemBInUSD);

            Assert.Equal(2, cart.Items.Count());
        }

        [Fact]
        public void Multiple_item_additions_cart_should_update_item_collection()
        {
            var cart = new Cart(Currency);
            var itemA = ItemAInUSD;//Capture reference of item object returned by property ItemAInUSD
            var itemB = ItemBInUSD;//Capture reference of item object returned by property ItemBInUSD
            cart.Add(itemA);
            cart.Add(itemB);

            Assert.Contains(itemA, cart.Items);
            Assert.Contains(itemB, cart.Items);

        }

        [Fact]
        public void Adding_new_item_to_cart_should_update_cart_total()
        {
            var cart = new Cart(Currency);
            cart.Add(ItemAInUSD);

            Assert.Equal(cart.Price, ItemAInUSD.Price);
        }
    }
}
