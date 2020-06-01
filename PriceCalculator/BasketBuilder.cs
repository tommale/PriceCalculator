
using PriceCalculator.Goods;
using System;

using System.Collections.Generic;
using System.Linq;
namespace PriceCalculator
{
    public class BasketBuilder
    {
        private string _errors = string.Empty;

        public BasketBuilder()
        {

        }

        public string ParseError
        {
            get { return _errors; }

        }

        public bool Parse(string[] args, out Basket basket)
        {
            basket = new Basket();
            var canParse = true;
            if (args == null)
            {
                canParse = false;
                _errors += "No Items Given to Price Calculator";
                return canParse;
            }


            var allGoodsCurrentlySold = GetAllGoodsCurrentlySold();
            foreach (var goodname in args)
            {
                var good = allGoodsCurrentlySold.FirstOrDefault(x => x.GoodName.ToUpper() == goodname.ToUpper());
                if (good == null)
                {
                    canParse = false;
                    _errors = $"Could not find '{goodname}', we dont sell this";
                    return canParse;
                }

               basket.Add(good);
                
            }


            return canParse;
        }


        private List<Good> GetAllGoodsCurrentlySold()
        // Note using reflection but this a little over kill, we could just have a case statment!!
        // Prefer to kiss (Keep It Simple Stupid)
        {
            return typeof(Good)
     .Assembly.GetTypes()
     .Where(t => t.IsSubclassOf(typeof(Good)) && !t.IsAbstract)
     .Select(t => (Good)Activator.CreateInstance(t)).ToList();

        }
    }
}
