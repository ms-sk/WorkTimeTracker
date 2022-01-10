using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkTimeTracker
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetValue<T>(ref T field, T value, [CallerMemberName()] string name = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}