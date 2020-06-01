namespace PriceCalculator.Goods
{
    public abstract class Good
    {
        public virtual string GoodName
        {            //  have made this virtual as its possible we might want to overwrite it.
                     // but i have defaulted it the type name
            get => GetType().Name;
        }


        public abstract int PricePerUnitInPence { get; }

     

    }
}
