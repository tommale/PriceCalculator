using PriceCalculator.Goods;
using System.Collections.Generic;

namespace PriceCalculator
{
    public interface IBasket
    {
        IEnumerable<GoodAndQuantity> goods { get; }
        void Add(Good good);

        public bool Contains(Good good, out GoodAndQuantity goodAndQuantity);
    }
}