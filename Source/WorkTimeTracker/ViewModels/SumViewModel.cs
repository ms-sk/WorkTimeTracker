namespace WorkTimeTracker.ViewModels
{
    public sealed class SumViewModel : ViewModel
    {
        public decimal Sum
        {
            get => GetValue<decimal>();
            set => SetValue(value);
        }

        public decimal BreakSum
        {
            get => GetValue<decimal>();
            set => SetValue(value);
        }

        public string DisplayText
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
    }
}
