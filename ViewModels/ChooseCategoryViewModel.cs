using Game_collection.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    class ChooseCategoryViewModel : INotifyPropertyChanged
    {

        // Implémentation de l'interface INotifyPropertyChanged pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;

        public string SettingsMessage => "Bienvenue dans la page category.";

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

        public ChooseCategoryViewModel()
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
