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

        // Commande liée à un bouton dans la vue
        
        private readonly MainViewModel _mainViewModel;

        public HomeViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Initialisation de la commande
            GoToBrowseGamesCommand = new RelayCommand(GoToChooseCategory);

            GoToChooseCollectionCommand = new RelayCommand(GoToChooseCollection);

            GoToWishlistCommand = new RelayCommand(GoToWishlist);

            _boxes = new ObservableCollection<Box>();
            Boxes = new ObservableCollection<BoxViewModel>
            {
                new BoxViewModel("JEUX ENREGISTRES", "Ensemble des jeux enregistrés sur la plateforme (indépendamment du genre et de la collection)", "GO", GoToBrowseGamesCommand),
                new BoxViewModel("COLLECTION","Choix de la collection à parcourir","GO", GoToChooseCollectionCommand),
                new BoxViewModel("WISHLIST", "Voir les jeux que vous souhaitez acquérir", "GO", GoToWishlistCommand),
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
            _mainViewModel.ChangeViewModel(new ChooseCollectionViewModel(_mainViewModel));
        }

        public ICommand GoToBrowseGamesCommand{ get; }
        // Méthode exécutée lorsque la commande est invoquée
        private void GoToChooseCategory()
        {
            _mainViewModel.ChangeViewModel(new BrowseGamesViewModel(_mainViewModel));
        }

        public ICommand GoToWishlistCommand { get; }
        private void GoToWishlist()
        {
            _mainViewModel.ChangeViewModel(new BrowseCollectionViewModel(_mainViewModel, "Wishlist"));
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
