using System;

namespace PriceCalculator
{
    public interface IPrinter
    {
        void PrintLine(string line);
    }

    public class ConsolePrinter : IPrinter
    {
        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }

    }
}