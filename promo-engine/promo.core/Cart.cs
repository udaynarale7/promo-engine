using System;
using System.Threading.Tasks;

namespace promo.core
{
    public class Cart
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="currency">
        /// It's the currency in which cart will operate.
        /// </param>
        public Cart(string currency)
        {
            currency.EnsureNotNullOrWhiteSpace(nameof(currency));

            Currency = currency;
        }

        public string Currency { get; }
        public string Id { get; set; }
        public decimal Price { get; set; }
        public DateTime UtcCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UtcUpdatedAt { get; set; }
        private bool IsNew => string.IsNullOrWhiteSpace(Id) == true;

        public async Task SaveToAsync(ICartStore cartStore)
        {
            if (IsNew == true)
            {
                var cart = await cartStore.CreateAsync(this);
            }
            else
            {
                await cartStore.UpdateAsync(Id, this);
            }
        }

        public static async Task<Cart> GetAsync(string cartId, ICartStore cartStore)
        {
            cartId.EnsureNotNullOrWhiteSpace(nameof(cartId));
            var cart = await cartStore.GetAsync(cartId);
            return cart;
        }
    }
}
