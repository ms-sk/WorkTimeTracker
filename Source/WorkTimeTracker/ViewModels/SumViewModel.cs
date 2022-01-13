namespace WorkTimeTracker.ViewModels
{
    internal sealed class SumViewModel : ViewModel
    {
        decimal _sum;
        decimal _breakSum;
        string _displayText = string.Empty;

        public decimal Sum
        {
            get => _sum;
            set => SetValue(ref _sum, value);
        }

        public decimal BreakSum
        {
            get => _breakSum;
            set => SetValue(ref _breakSum, value);
        }

        public string DisplayText
        {
            get => _displayText;
            set => SetValue(ref _displayText, value);
        }
    }
}
