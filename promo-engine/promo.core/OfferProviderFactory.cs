using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public class OfferProviderFactory : IOfferProviderFactory
    {
        public async Task<IOfferProvider> GetOfferProviderAsync(string contextId)
        {
            return await Task.FromResult(new OfferProviderV1());
        }
    }
}
