
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Tests
{
    public class BasketBuilderShould
    {

       
        [TestCase(new object[] { "Apples"}, 1)]
        [TestCase(new object[] { "Apples", "Beans" }, 2)]
        [TestCase(new object[] { "Apples", "Beans", "Bread" }, 3)]
        [TestCase(new object[] { "Apples", "Beans", "Bread", "Milk" }, 4)]
        [TestCase(new object[] { "Apples", "Apples", }, 1)]

        public void ParseBasket(object[] testArg, int expectedCount)
        {
            var args = new List<string>();
            foreach (var test in testArg) {
                args.Add(test.ToString());
            }

            var basketBuilder = new BasketBuilder();          
            Assert.True(basketBuilder.Parse(args.ToArray(), out var basket));
            Assert.That(basket.goods.Count(), Is.EqualTo(expectedCount)); ; ; 

        }

        [TestCase(new object[] { "", }, "Could not find")]
        [TestCase(new object[] { "newproduct", }, "Could not find ")]
    
        public void NotParseBasket(object[] testArg, string expectedError)
        {
            var args = new List<string>();
            foreach (var test in testArg)
            {
                args.Add(test.ToString());
            }

            var basketBuilder = new BasketBuilder();
       
            Assert.False(basketBuilder.Parse(args.ToArray(), out var basket));
            StringAssert.StartsWith( expectedError, basketBuilder.ParseError);

        }

        [Test]
        public void ReportErrorParseBasketIfNoArgs()
        { 
            var basketBuilder = new BasketBuilder();
            var basket = new Basket();
            Assert.False(basketBuilder.Parse(null, out basket)); ;
            StringAssert.AreEqualIgnoringCase( "No Items Given to Price Calculator", basketBuilder.ParseError);

        }


    }
}