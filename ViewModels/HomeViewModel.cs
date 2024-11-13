using Game_collection.Utils;
using Game_collection.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        
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
        public ICommand GoToChooseCategoryCommand { get; }
        
        private readonly MainViewModel _mainViewModel;

        public HomeViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Initialisation de la commande
            GoToChooseCategoryCommand = new RelayCommand(GoToChooseCategory);

            GoToChooseCollectionCommand = new RelayCommand(GoToChooseCollection);
            Message = "Bienvenue dans la page home";

            _boxes = new ObservableCollection<Box>();
            Boxes = new ObservableCollection<BoxViewModel>
            {
                new BoxViewModel("JEUX DISPO SUR L’APPLICATION", "Description pour la box 1", "GO", GoToChooseCategoryCommand),
                new BoxViewModel("COLLECTION","Description pour la box 2","GO", GoToChooseCollectionCommand),
                new BoxViewModel("WISHLIST", "Description pour la box 3", "GO", GoToChooseCategoryCommand),
            };
        }


        ObservableCollection<Box> _boxes;
        public ObservableCollection<BoxViewModel> Boxes
        {
            get;
            set;
        }

        public ICommand GoToChooseCollectionCommand { get; }

        private void GoToChooseCollection()
        {
            _mainViewModel.ChangeViewModel(new ChooseCollectionViewModel());
        }

        // Méthode exécutée lorsque la commande est invoquée
        private void GoToChooseCategory()
        {
            _mainViewModel.ChangeViewModel(new ChooseCategoryViewModel());
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
