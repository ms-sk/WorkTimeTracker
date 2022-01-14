namespace Core.Math
{
    public sealed class Calculator : ICalculator
    {
        public decimal CalculateBreak(decimal hours)
        {
            decimal breakTime;
            if (hours <= 6M)
            {
                breakTime = 0;
            }
            else if (hours > 6M && hours <= 9.5M)
            {
                breakTime = 0.5M;
            }
            else if (hours > 9.75M && hours < 11M)
            {
                breakTime = 0.75M;
            }
            else
            {
                breakTime = 1M;
            }

            return breakTime;
        }

        public decimal RoundQuarter(double value)
        {
            return (decimal)(System.Math.Round(value * 4, MidpointRounding.ToEven) / 4.0);
        }
    }
}
