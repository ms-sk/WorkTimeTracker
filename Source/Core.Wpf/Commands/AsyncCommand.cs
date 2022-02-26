using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Wpf.Commands;

public sealed class AsyncCommand : ICommand
{
    Func<object?, Task> _executeCallback;
    Func<object?, bool>? _canExecuteCallback;

    public AsyncCommand(Func<object?, Task> executeCallback, Func<object?, bool> canExecuteCallback)
    {
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if (_canExecuteCallback == null)
        {
            return true;
        }

        return _canExecuteCallback(parameter);
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        await _executeCallback.Invoke(parameter);
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}