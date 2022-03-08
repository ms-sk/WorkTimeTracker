using System;
using System.Windows.Input;

namespace WorkTimeTracker.Core.Wpf.Commands;

public sealed class Command : ICommand
{
    readonly Action<object?> _executeCallback;
    readonly Func<object?, bool>? _canExecuteCallback;
    readonly Action? _callBack;

    public Command(Action<object?> executeCallback, Func<object?, bool>? canExecuteCallback)
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

    public void Execute(object? parameter)
    {
        _executeCallback(parameter);
    }


    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}