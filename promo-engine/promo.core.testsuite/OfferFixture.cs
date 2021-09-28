using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace promo.core.testsuite
{
    public class OfferFixture
    {
        private readonly string Currency = "USD";
        public CartItem ItemA(int productCount = 1)
        {

            var productKey = new ProductKey("A", "A");
            var product = new Product(productKey);
            return new CartItem(product) { Price = 50, ProductCount = productCount };
        }

        public CartItem ItemB(int productCount = 1)
        {

            var productKey = new ProductKey("B", "B");
            var product = new Product(productKey);
            return new CartItem(product) { Price = 30, ProductCount = productCount };

        }
        public CartItem ItemC(int productCount = 1)
        {

            var productKey = new ProductKey("C", "C");
            var product = new Product(productKey);
            return new CartItem(product) { Price = 20, ProductCount = productCount };

        }
        public CartItem ItemD(int productCount = 1)
        {
            var productKey = new ProductKey("D", "D");
            var product = new Product(productKey);
            return new CartItem(product) { Price = 15, ProductCount = productCount };
        }

        [Fact]
        public async Task Apply_offer_to_cart_should_apply_offer_50_USD_with_5A_5B_1C()
        {
            var cart = new Cart(Currency);
            cart.Add(ItemA(5));
            cart.Add(ItemB(5));
            cart.Add(ItemC(1));
            var offer = await Offer.GetOfferAsync(cart, "ABC", new OfferProviderFactory(), "global");
            Assert.Equal(50, offer.Amount);
        }


        [Fact]
        public async Task Apply_offer_to_cart_should_apply_offer_55_USD_with_3A_5B_1C_1D()
        {
            var cart = new Cart(Currency);
            cart.Add(ItemA(3));
            cart.Add(ItemB(5));
            cart.Add(ItemC(1));
            cart.Add(ItemD(1));
            var offer = await Offer.GetOfferAsync(cart, "ABC", new OfferProviderFactory(), "global");
            Assert.Equal(55, offer.Amount);
        }
    }
}
