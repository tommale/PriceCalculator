using System.Collections.Generic;

namespace PriceCalculator
{
    public interface ISpecialOffers
    {
        bool CanApply(IBasket basket, out List<AppliedSpecialOffers> appliedSecialOffers);

    }

}