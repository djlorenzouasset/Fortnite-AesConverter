using System;
using System.Windows.Input;

namespace FortniteKeyChain.ViewModels;

public class MenuCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private Action<object> _action;
    private Func<bool> _canExecute;

    public MenuCommand(Action<object> action, Func<bool> canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute.Invoke();
    }

    public void Execute(object parameter) 
    {
        _action(parameter);
    }
}