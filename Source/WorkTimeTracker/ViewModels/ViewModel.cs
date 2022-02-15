using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkTimeTracker.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, object> _propertyStore = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public T GetValue<T>([CallerMemberName] string propertyName = "")
        {
            if (_propertyStore.ContainsKey(propertyName))
            {
                return (T) _propertyStore[propertyName];
            }

            return default;
        }
        
        public void SetValue<T>(T value, [CallerMemberName] string propertyName = "")
        {
            _propertyStore[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}