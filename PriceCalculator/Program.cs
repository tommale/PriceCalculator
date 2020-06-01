using System;

namespace PriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Note we could use DI, but as its only simple application its not needed.

            var basketBuilder = new BasketBuilder();
            var specialOffers = new SpecialOffers();
            IPrinter printer = new ConsolePrinter();    

            if (basketBuilder.Parse(args, out var basket))
            {
                var priceCalculator = new PriceCalculator(printer, specialOffers);
                priceCalculator.Calculate(basket);
            }
            else {
                printer.PrintLine(basketBuilder.ParseError);
            };
        }
    }
}
