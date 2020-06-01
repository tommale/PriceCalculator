
namespace PriceCalculator
{
    public class PriceCalcuatlor
    {
        private readonly IPrinter _printer;
        private readonly ISpecialOffers _specialOffers;

        public PriceCalcuatlor(IPrinter printer, ISpecialOffers specialOffers)
        {
            _printer = printer;
            _specialOffers = specialOffers;
        }

        public void Calcualte(IBasket basket)
        {
            var subtotalInPence = 0;

            foreach (var item in basket.goods)
            {
                subtotalInPence += (item.Good.PricePerUnitInPence * item.Quantity);
            }

            double subtotalInPounds = subtotalInPence / 100d;
            _printer.PrintLine($"Subtotal: {subtotalInPounds.ToString("C2")}");

            if (_specialOffers.CanApply(basket, out var appliedSpecialOffers))
            {
                var totalSavingInPence = 0d;
                foreach (var appliedoffers in appliedSpecialOffers) {
                    _printer.PrintLine($"{appliedoffers.Name}: {appliedoffers.SavingInPence}p");
                    totalSavingInPence += appliedoffers.SavingInPence;
                }
                 var totalInPounds = (subtotalInPence - totalSavingInPence) / 100d;

                _printer.PrintLine($"Total price: {totalInPounds.ToString("C2")}");
            }
            else
            {
                _printer.PrintLine("(No offers available)");
                _printer.PrintLine($"Total price: {subtotalInPounds.ToString("C2")}");
            }


        }

    }
}