using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public class Offer
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public decimal Amount { get; set; }

        public static async Task<Offer> GetOfferAsync(Cart request,string promoCode, IOfferProviderFactory offerProviderFactory, string contextId)
        {
            try
            {
                var provider = await offerProviderFactory.GetOfferProviderAsync(contextId);
                return await provider.GetOfferAsync(request, promoCode);
            }
            catch(Exception ex)
            {
                //TODO:need proper exception handling
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
