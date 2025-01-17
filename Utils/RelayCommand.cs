using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object> _executeWithParameter; // Pour les commandes avec paramètre
    private readonly Action _executeWithoutParameter;      // Pour les commandes sans paramètre
    private readonly Func<object, bool> _canExecuteWithParameter;
    private readonly Func<bool> _canExecuteWithoutParameter;

    public event EventHandler CanExecuteChanged;

    // Constructeur pour les commandes avec paramètre
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _executeWithParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecuteWithParameter = canExecute;
    }

    // Constructeur pour les commandes sans paramètre
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _executeWithoutParameter = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecuteWithoutParameter = canExecute;
    }

    // Détermine si la commande peut s'exécuter
    public bool CanExecute(object parameter)
    {
        if (_canExecuteWithoutParameter != null)
            return _canExecuteWithoutParameter();
        if (_canExecuteWithParameter != null)
            return _canExecuteWithParameter(parameter);
        return true;
    }

    // Exécute la commande
    public void Execute(object parameter)
    {
        if (_executeWithoutParameter != null)
            _executeWithoutParameter();
        else if (_executeWithParameter != null)
            _executeWithParameter(parameter);
    }

    // Méthode pour notifier les changements
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
