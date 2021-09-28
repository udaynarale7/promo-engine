using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace promo.core.testsuite
{
    public class OfferServiceFixture
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
        public async Task Apply_non_matching_offer_should_return_cart_without_any_offer()
        {
            var cartStore = new MockCartStore();
            var cart = new Cart(Currency);
            cart.Add(ItemA());
            cart.Add(ItemB());
            cart.Add(ItemC());
            await cart.SaveToAsync(cartStore);
            OfferService offerService = new OfferService(new MockCartStore(), new OfferProviderFactory());
            var cartRS = await offerService.ApplyOfferAsync(cart.Id, "ABC");
            Assert.Equal(cartRS.Price, 100);
        }

        [Fact]
        public async Task Apply_offer_to_cart_should_apply_offer_50_USD_with_5A_5B_1C()
        {
            var cartStore = new MockCartStore();
            var cart = new Cart(Currency);
            cart.Add(ItemA(5));
            cart.Add(ItemB(5));
            cart.Add(ItemC(1));
            await cart.SaveToAsync(cartStore);
            OfferService offerService = new OfferService(new MockCartStore(), new OfferProviderFactory());
            var cartRS = await offerService.ApplyOfferAsync(cart.Id, "ABC");
            Assert.Equal(cartRS.Price, 370);
        }


        [Fact]
        public async Task Apply_offer_to_cart_should_apply_offer_55_USD_with_3A_5B_1C_1D()
        {
            var cartStore = new MockCartStore();
            var cart = new Cart(Currency);
            cart.Add(ItemA(3));
            cart.Add(ItemB(5));
            cart.Add(ItemC(1));
            cart.Add(ItemD(1));
            await cart.SaveToAsync(cartStore);
            OfferService offerService = new OfferService(new MockCartStore(), new OfferProviderFactory());
            var cartRS = await offerService.ApplyOfferAsync(cart.Id, "ABC");
            Assert.Equal(cartRS.Price, 280);
        }
    }
}
