using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.Wpf.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, object?> _propertyStore = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected T? GetValue<T>([CallerMemberName] string propertyName = "")
        {
            if (_propertyStore.ContainsKey(propertyName))
            {
                var value = _propertyStore[propertyName];
                if (value != null)
                {
                    return (T) value;
                }
            }

            return default;
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = "")
        {
            _propertyStore[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}