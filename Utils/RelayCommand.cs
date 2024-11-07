using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.Utils
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Événement requis par l'interface ICommand
        public event EventHandler CanExecuteChanged;

        // Constructeur qui prend l'action à exécuter et une condition facultative
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Détermine si la commande peut s'exécuter
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // Exécute la commande
        public void Execute(object parameter)
        {
            _execute();
        }

        // Méthode pour notifier un changement dans l'état de la commande (optionnel)
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
