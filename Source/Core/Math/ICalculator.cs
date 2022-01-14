namespace Core.Math
{
    public interface ICalculator
    {
        decimal RoundQuarter(double value);

        decimal CalculateBreak(decimal hours);
    }
}