using Resources;
using System.Windows;

namespace Core.Wpf.MessageBoxes
{
    public sealed class CMessageBox
    {
        public static MessageBoxResult Delete()
        {
            return MessageBox.Show(Translations.AreYouSure, Translations.Delete, MessageBoxButton.YesNo);
        }
    }
}
