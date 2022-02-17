using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTimeTracker.ViewModels;

public sealed class AsyncCommand : ICommand
{
    private Func<object?, Task> _executeCallback;
    private Func<object?, bool>? _canExecuteCallback;

    public AsyncCommand(Func<object?, Task> executeCallback, Func<object?, bool> canExecuteCallback)
    {
        _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        _canExecuteCallback = canExecuteCallback;
    }
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

    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}