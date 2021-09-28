using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public interface ICartStore
    {
        Task<Cart> CreateAsync(Cart cart);

        Task<Cart> UpdateAsync(string id, Cart cart);

        Task<Cart> GetAsync(string cartId);
    }
}
