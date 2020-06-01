
using PriceCalculator.Goods;
using System.Collections.Generic;
using System.Linq;


namespace PriceCalculator
{
    public class Basket : IBasket
    {
        readonly List<GoodAndQuantity> _goodAndQuantity = new List<GoodAndQuantity>();

     
        public IEnumerable<GoodAndQuantity> goods
        {
            get => _goodAndQuantity;
        }

        public bool Contains(Good good, out GoodAndQuantity goodAndQuantity)
        {

            goodAndQuantity = _goodAndQuantity.FirstOrDefault(x => x.Good.GoodName == good.GoodName);
            return goodAndQuantity != null;
        }

        public void Add(Good good)
        {

            if (!_goodAndQuantity.Any(x => x.Good.GoodName == good.GoodName))
            {
                _goodAndQuantity.Add(new GoodAndQuantity() { Good = good, Quantity = 1 });
            }
            else
            {
                _goodAndQuantity.First(x => x.Good.GoodName == good.GoodName).Quantity++;
            }
        }
    }

    public class GoodAndQuantity
    {

        public Good Good { get; set; }
        public int Quantity { get; set; }

    }



}
