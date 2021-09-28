using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public interface IOfferProviderFactory
    {
        Task<IOfferProvider> GetOfferProviderAsync(string contextId);
    }
}
