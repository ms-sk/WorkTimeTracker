using Core.Wpf.ViewModels;
using System.Windows;

namespace Core.Wpf.MessageBoxes
{
    public sealed class CoreMessageBoxViewModel : ViewModel
    {
        public string Title
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }
        public string Message
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }

        public MessageBoxResult Result { get; set; } = MessageBoxResult.Cancel;
    }
}
