using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promo.core
{
    public class OfferProviderV1 : IOfferProvider
    {
        public async Task<Offer> GetOfferAsync(Cart offerRequest, string promoCode)
        {
            //1.GetQualfiedRules
            var rules = GetQualfiedRules(offerRequest, promoCode);
            ////2.Find Max score rule
            //var rule = rules.OrderByDescending(y => y.Score).First();
            //3.Translate to offer
            return await Task.FromResult(TranslateToOffer(rules, offerRequest));
        }

        private static Offer TranslateToOffer(List<Rule> rules, Cart offerRequest)
        {
            decimal discountAmount = 0;
            foreach (var rule in rules)
            {
                discountAmount += rule.DiscountType == DiscountType.Fixed ? rule.Discount : offerRequest.Price * rule.Discount / 100;

            }
            return new Offer()
            {
                Amount = discountAmount,
                Code = rules.First().PromoCode,
                Desc = rules.First().Desc,
                Type = rules.First().DiscountType.ToString()
            };
        }

        public List<Rule> GetQualfiedRules(Cart request, string promoCode)
        {
            List<Rule> rules = GetActiveMatchingRules(promoCode);
            List<Rule> qualifiedRules = new List<Rule>();
            foreach (var rule in rules)
            {
                int matchingProductRuleCount = 0;
                int addSameRuleCount = 1;
                foreach (var ruleProduct in rule.Products)
                {
                    foreach (var item in request.Items)
                    {
                        var productType = item.Product.ProductKey.ProductType;
                        int productCount = item.ProductCount;
                        if (ruleProduct.Type.Equals(productType) && ruleProduct.Count <= productCount)
                        {
                            matchingProductRuleCount++;
                            if (ruleProduct.Count < productCount)
                            {
                                int productRemainingcount = productCount - ruleProduct.Count;
                                int ruleCount = productRemainingcount / ruleProduct.Count;
                                if (ruleCount > 0)
                                    addSameRuleCount += ruleCount;
                            }
                        }
                    }
                }
                if (matchingProductRuleCount == rule.Products.Count)
                {
                    for (int i = 0; i < addSameRuleCount; i++)
                        qualifiedRules.Add(rule);
                }
            }
            if (qualifiedRules.Any() == false)
                throw new ApplicationException("No active promo code found");
            return qualifiedRules;
        }

        private List<Rule> GetActiveMatchingRules(string promoCode)
        {
            var rules = GetRules();
            var promoMatchingRules = rules.Select(x => x.PromoCode.Equals(promoCode, StringComparison.InvariantCultureIgnoreCase) && x.IsActive == true);
            if (promoMatchingRules.Any() == false)
                throw new NotSupportedException("Given promo code is not valid");
            return rules;
        }

        public List<Rule> GetRules()
        {
            return new List<Rule>()
            {
                new Rule(){PromoCode="ABC",Desc="ABC discount", Discount=20,DiscountType=DiscountType.Fixed,Products= new List<Product>(){
                new Product(){ Type="A",Count=3}},Score=1,IsActive=true } ,
               new Rule(){ PromoCode="ABC",Desc="ABC discount",Discount=15,DiscountType=DiscountType.Fixed,Products= new List<Product>(){
                new Product(){ Type="B",Count=2}},Score=1,IsActive=true } ,
                 new Rule(){PromoCode="ABC", Desc="ABC discount",Discount=5,DiscountType=DiscountType.Fixed,Products= new List<Product>(){
                new Product(){ Type="C",Count=1},
                 new Product(){ Type="D",Count=1}},Score=1,IsActive=true } ,
                //new Rule(){ PromoCode="ABC",Desc="ABC discount",Discount=20,DiscountType=DiscountType.Pecentage,Products= new List<Product>(){
                //new Product(){ Type="A",Count=3}},Score=2,IsActive=true } ,
            };
        }
        public class Rule
        {
            public string PromoCode { get; set; }
            public List<Product> Products { get; set; }
            public decimal Discount { get; set; }
            public DiscountType DiscountType { get; set; }
            public int Score { get; set; }
            public string Desc { get; set; }
            public bool IsActive { get; set; }
        }

        public enum DiscountType
        {
            Fixed,
            Pecentage
        }
        public class Product
        {
            public string Type { get; set; }
            public int Count { get; set; }
        }
    }
}
