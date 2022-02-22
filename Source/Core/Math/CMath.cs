namespace Core.Math
{
    public static class CMath
    {
        public static double CalculateBreak(double hours)
        {
            double breakTime;
            if (hours <= 6.0)
            {
                breakTime = 0;
            }
            else if (hours is > 6.0 and <= 9.5)
            {
                breakTime = 0.5;
            }
            else
            {
                breakTime = 0.75;
            }

            return breakTime;
        }

        public static double RoundQuarter(double value)
        {
            return System.Math.Round(value * 4, MidpointRounding.ToEven) / 4.0;
        }
    }
}
