using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace promo.core
{
    public static class Ensure
    {
        public static void EnsureNotNull<T>(this T target, string name)
        {
            if (target == null)
                throw new ArgumentNullException(name);

        }

        public static void EnsureNotNullOrWhiteSpace(this string target, string name)
        {
            if (string.IsNullOrWhiteSpace(target) == true)
                throw new ArgumentNullException(name);
        }

        public static Cart EnsureExist(this Cart cart, string cartId)
        {
            if (cart == null)
                throw new KeyNotFoundException(cartId);
            return cart;
        }
        public static Cart EnsureCanAddOffer(this Cart cart)
        {
            if (cart.Items == null || cart.Items.Any() == false)
                throw new InvalidOperationException(cart.Id);

            return cart;
        }
    }

}