using Resources;
using System.Windows;

namespace Core.Wpf.MessageBoxes
{
    public sealed class CMessageBox
    {
        public static MessageBoxResult Delete()
        {
            var messageBox = new CoreMessageBox();
            var viewModel = new CoreMessageBoxViewModel()
            {
                Title = Translations.AreYouSure,
                Message = Translations.Delete
            };

            messageBox.DataContext = viewModel;

            messageBox.ShowDialog();

            return viewModel.Result;
        }
    }
}
