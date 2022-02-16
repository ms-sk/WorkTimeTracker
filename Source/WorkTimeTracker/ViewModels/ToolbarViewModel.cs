namespace WorkTimeTracker.ViewModels
{
    public sealed class ToolbarViewModel
    {
        public ToolbarViewModel(SumViewModel sum)
        {
            Sum = sum ?? throw new System.ArgumentNullException(nameof(sum));
            Sum.DisplayText = "8.25/ 0.00";
        }

        public SumViewModel Sum { get; }

    }
}