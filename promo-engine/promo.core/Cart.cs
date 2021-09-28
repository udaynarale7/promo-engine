using System;
using System.Collections.Generic;
using System.Linq;
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
        public decimal Price => Items.Sum(x => x.Price * x.ProductCount) - _offers.Sum(x => x.Amount);
        public DateTime UtcCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UtcUpdatedAt { get; set; }
        private bool IsNew => string.IsNullOrWhiteSpace(Id) == true;

        private readonly List<CartItem> _items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get => new List<CartItem>(_items);
            internal set
            {
                if (value == null || value.Any() == false)
                {
                    _items.Clear();
                }
                else
                    _items.AddRange(value.ToList());
            }
        }

        public async Task SaveToAsync(ICartStore cartStore)
        {
            if (IsNew == true)
            {
                await cartStore.CreateAsync(this);
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

        public void Add(CartItem item)
        {
            _items.Add(item);
        }

        public IEnumerable<Offer> Offers
        {
            get => new List<Offer>(_offers);
            internal set
            {
                if (value == null || value.Any() == false)
                {
                    _offers.Clear();
                }
                else
                    _offers.AddRange(value.ToList());
            }
        }

        private readonly List<Offer> _offers = new List<Offer>();
        public void AddOffer(Offer offer)
        {
            _offers.Add(offer);
        }
    }
}
