using System;

namespace PriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var basketBuilder = new BasketBuilder();
            var specialOffers = new SpeicalOffers();
            IPrinter printer = new ConsolePrinter();

            if (basketBuilder.Parse(args, out var basket))
            {
                var priceCalcuatlor = new PriceCalcuatlor(printer, specialOffers);
                priceCalcuatlor.Calcualte(basket);
            }
            else {
                printer.PrintLine(basketBuilder.ParseError);
             

            };

          
        }
    }
}
