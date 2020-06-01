using NSubstitute;
using NUnit.Framework;
using PriceCalculator.Goods;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PriceCalculator.Tests
{
    public class PriceCalculatorShould
    {

        [Test]
        public void CalculateWithNoOffers()
        {
            var printer = Substitute.For<IPrinter>();
            var basket = Substitute.For<IBasket>();
            var specialOffers = new SpeicalOffers();

            basket.goods.Returns(x => new List<GoodAndQuantity>() {
                new GoodAndQuantity {Quantity = 2, Good = new Apples()  },
                new GoodAndQuantity {Quantity = 1, Good = new Beans()  } });

          

            var priceCalcuatlor = new PriceCalcuatlor(printer, specialOffers);

            priceCalcuatlor.Calcualte(basket);

            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Subtotal:")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("(No offers available)")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Total price: ")));

        }

        [Test]
        public void CalculateWithAppleOffers()
        {
            var printer = Substitute.For<IPrinter>();
            var basketBuilder = new BasketBuilder();
            basketBuilder.Parse(new string[] { "Apples", "Beans", "Bread", "Milk" }, out var basket);
            var specialOffers = new SpeicalOffers();

            var priceCalcuatlor = new PriceCalcuatlor(printer, specialOffers);

            priceCalcuatlor.Calcualte(basket);

            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Subtotal:")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Apples 10% off:")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Total price: ")));

        }

        [Test]
        public void CalculateWithLotsOfAppleOffers()
        {
            var printer = Substitute.For<IPrinter>();
            var basketBuilder = new BasketBuilder();
            basketBuilder.Parse(new string[] { "Apples", "Apples", "Apples", "Apples" }, out var basket);
            var specialOffers = new SpeicalOffers();

            var priceCalcuatlor = new PriceCalcuatlor(printer, specialOffers);

            priceCalcuatlor.Calcualte(basket);

            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Subtotal: ")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Apples 10% off:: 40p")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Total price: ")));

        }

        [Test]
        public void CalculateWithBreadAndBeansOffers()
        {
            var printer = Substitute.For<IPrinter>();
            var basketBuilder = new BasketBuilder();
            basketBuilder.Parse(new string[] { "Beans", "Beans", "Bread" }, out var basket);
            var specialOffers = new SpeicalOffers();

            var priceCalcuatlor = new PriceCalcuatlor(printer, specialOffers);

            priceCalcuatlor.Calcualte(basket);

            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Subtotal")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Buy 2 cans of ")));
            printer.Received().PrintLine(Arg.Is<string>(x => x.StartsWith("Total price:")));

        }
    }
}