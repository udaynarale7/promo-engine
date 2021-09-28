using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public class MockCartStore : ICartStore
    {
        private static ConcurrentDictionary<string, Cart> _cartStore = new ConcurrentDictionary<string, Cart>();

        public async Task<Cart> CreateAsync(Cart cart)
        {
            //assign id to cart
            cart.Id = Guid.NewGuid().ToString().Substring(0, 12);

            //assign id for item and payment options in cart
            cart.Items.ToList().ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x.Id))
                    x.Id = Guid.NewGuid().ToString();



            });

            _cartStore.TryAdd(cart.Id, cart);

            var currentTimestamp = DateTime.UtcNow;
            cart.UtcCreatedAt = currentTimestamp;
            cart.UtcUpdatedAt = currentTimestamp;

            return await Task.FromResult(cart);
        }

        public async Task<Cart> GetAsync(string cartId)
        {
            if (_cartStore.ContainsKey(cartId))
                return await Task.FromResult(_cartStore[cartId]);

            return null;
        }

        public async Task<Cart> UpdateAsync(string id, Cart cart)
        {

            //assign id for item and payment options in cart
            cart.Items.ToList().ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x.Id))
                    x.Id = Guid.NewGuid().ToString();
            });

            _cartStore.TryRemove(id, out Cart value);
            _cartStore.TryAdd(id, cart);

            var currentTimestamp = DateTime.UtcNow;
            cart.UtcUpdatedAt = currentTimestamp;

            return await Task.FromResult(cart);
        }
    }
}
