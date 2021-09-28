using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public interface IOfferProvider
    {
        Task<Offer> GetOfferAsync(Cart offerRequest, string promoCode);
    }
}
