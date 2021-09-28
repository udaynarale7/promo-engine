using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public class OfferService
    {
        readonly ICartStore _cartStore;
        readonly IOfferProviderFactory _offerProviderFactory;
        public OfferService(ICartStore cartStore, IOfferProviderFactory offerProviderFactory)
        {
            _cartStore = cartStore;
            _offerProviderFactory = offerProviderFactory;
        }
        public async Task<Cart> ApplyOfferAsync(string cartId, string offerCode)
        {
            var cart = await Cart.GetAsync(cartId, _cartStore);
            cart.EnsureExist(cartId);
            cart.EnsureCanAddOffer();

            //Get offer from provider
            var offerProviderResponse = await Offer.GetOfferAsync(cart, offerCode, _offerProviderFactory, "global");
            if (offerProviderResponse != null)
            {
                //Add offer
                cart.AddOffer(offerProviderResponse);

                //Save cart
                await cart.SaveToAsync(_cartStore);
            }

            return cart;
        }
    }
}
