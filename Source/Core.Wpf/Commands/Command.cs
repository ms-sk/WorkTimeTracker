using System;
using System.Windows.Input;

namespace Core.Wpf.Commands;

/// <summary>
/// Defines a WPF command.
/// </summary>
public sealed class Command : ICommand
{
    readonly Action<object?> _executeCallback;
    readonly Func<object?, bool>? _canExecuteCallback;

    /// <summary>
    /// Initializes the WPF command.
    /// </summary>
    /// <param name="executeCallback">The execute callback.</param>
    /// <param name="canExecuteCallback">The can execute callback.</param>
    public Command(Action<object?> executeCallback, Func<object?, bool>? canExecuteCallback)
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

    public void Execute(object? parameter)
    {
        _executeCallback(parameter);
    }

    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}