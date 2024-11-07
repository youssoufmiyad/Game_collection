using Game_collection.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        private INotifyPropertyChanged _currentViewModel;

        public INotifyPropertyChanged CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        // Implémentation de l'interface INotifyPropertyChanged pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;

        public string WelcomeMessage => "Bienvenue sur la page d'accueil !";

        // Exemple de propriété simple liée à la vue
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commande liée à un bouton dans la vue
        public ICommand MettreAJourMessageCommand { get; }

        public HomeViewModel()
        {
            // Initialisation de la commande
            MettreAJourMessageCommand = new RelayCommand(MettreAJourMessage);
            Message = "Bienvenue dans la page home";
        }

        // Méthode exécutée lorsque la commande est invoquée
        private void MettreAJourMessage()
        {
            Message = "Message mis à jour !";
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
