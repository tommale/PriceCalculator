using PriceCalculator.Goods;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PriceCalculator
{
    public class AppliedSpecialOffers
    {
        public string Name { get; set; }
        public double SavingInPence { get; set; }
    }

    public class SpecialOffers : ISpecialOffers
    {

        public bool CanApply(IBasket basket, out List<AppliedSpecialOffers> appliedSpecialOffers)
        {
            var canApply = false;
            appliedSpecialOffers = new List<AppliedSpecialOffers>();
            #region Apples offer
            if (basket.Contains(new Apples(), out var applesPurchased))
            {
                canApply = true;
                appliedSpecialOffers.Add(new AppliedSpecialOffers() {
                    Name = "Apples 10% off:", 
                    SavingInPence = 0.10 * applesPurchased.Quantity * applesPurchased.Good.PricePerUnitInPence });
            };
            #endregion
            #region BeansAndBread offer
            if (basket.Contains(new Beans(), out var beansPurchased) &&
                basket.Contains(new Bread(), out var breadPurchased))
            {
                if (beansPurchased.Quantity >= 2)
                {

                    canApply = true;
                    appliedSpecialOffers.Add(new AppliedSpecialOffers()
                    {   // note we are only sellling 1 bread at half price, even if they buy 1000 cans of beans.
                        Name = "Buy 2 cans of Bean and get a loaf of bread for half price",
                        SavingInPence = 0.50 * breadPurchased.Good.PricePerUnitInPence
                    });
                }
            };
            #endregion

            return canApply;
        }
    }
}