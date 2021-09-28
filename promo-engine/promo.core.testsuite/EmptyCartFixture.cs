using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace promo.core.testsuite
{
    public class EmptyCartFixture
    {
        private readonly string Currency = "USD";

        [Fact]
        public void Newly_created_cart_should_have_empty_id()
        {
            var cart = new Cart(Currency);
            Assert.Null(cart.Id);
        }

        [Fact]
        public void Cart_with_invalid_currency_should_throw_invalid_currency_exception()
        {
            string currency = null;
            Action act = () => { new Cart(currency); };
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Cart_should_set_passed_currency()
        {
            string currency = "USD";
            var cart = new Cart(Currency);
            Assert.Equal(cart.Currency, currency);
        }

        [Fact]
        public void Newly_created_cart_should_have_zero_price()
        {
            var cart = new Cart(Currency);
            Assert.Equal(0, cart.Price);
        }


        [Fact]
        public async Task Persisted_cart_should_have_valid_id()
        {
            var cart = new Cart(Currency);
            Cart created = null;
            var storeMock = new Mock<ICartStore>();
            storeMock.Setup(s => s.CreateAsync(It.IsAny<Cart>()))
                     .Callback(() => { cart.Id = Guid.NewGuid().ToString(); created = cart; })
                     .ReturnsAsync(cart);

            ICartStore cartStore = storeMock.Object;
            await cart.SaveToAsync(cartStore);

            Assert.NotEmpty(cart.Id);
            storeMock.Verify(x => x.CreateAsync(cart), Times.Once());
        }
    }
}
