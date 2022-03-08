using System.Windows;
using WorkTimeTracker.Resources;

namespace WorkTimeTracker.Core.Wpf.MessageBoxes
{
    public sealed class CMessageBox
    {
        public static MessageBoxResult Delete()
        {
            var messageBox = new CoreMessageBox()
            {
                Owner = Application.Current.MainWindow
            };

            var viewModel = new CoreMessageBoxViewModel()
            {
                Header = Translations.AreYouSure,
                Message = Translations.DoYouWantToDelete
            };

            messageBox.DataContext = viewModel;

            viewModel.Executed += (_, __) => messageBox?.Close();

            messageBox.ShowDialog();

            return viewModel.Result;
        }
    }
}
