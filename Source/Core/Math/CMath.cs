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
            else if (hours > 6.0 && hours <= 9.5)
            {
                breakTime = 0.5;
            }
            else if (hours > 9.75 && hours < 11)
            {
                breakTime = 0.75;
            }
            else
            {
                breakTime = 1;
            }

            return breakTime;
        }

        public static double RoundQuarter(double value)
        {
            return System.Math.Round(value * 4, MidpointRounding.ToEven) / 4.0;
        }
    }
}
