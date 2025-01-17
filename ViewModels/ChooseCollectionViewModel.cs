using Game_collection.Database;
using Game_collection.Utils;
using Game_collection.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    public class ChooseCollectionViewModel : INotifyPropertyChanged
    {

        // Implémentation de l'interface INotifyPropertyChanged pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;


        private readonly MainViewModel _mainViewModel;

        string _newCollectionName;
        public string NewCollectionName
        {
            get { return _newCollectionName; }
            set
            {
                _newCollectionName = value;
                OnPropertyChanged("NewCollectionName");
            }
        }

        ObservableCollection<BigCardViewModel> _collectionCards;

        public ObservableCollection<BigCardViewModel> CollectionCards
        {
            get { return _collectionCards; }
            set
            {
                _collectionCards = value;
                OnPropertyChanged("CollectionCards");
            }
        }

        public ChooseCollectionViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            GoToBrowseCollectionCommand = new RelayCommand(() => GoToBrowseCollection("test"));

            AddNewCollectionCommand = new RelayCommand(AddNewCollection);

            CollectionCards = new ObservableCollection<BigCardViewModel>();

            List<string> collections = DataAccess.GetCollections();

            foreach (var collection in collections)
            {
                CollectionCards.Add(new BigCardViewModel(collection, "Chercher", new RelayCommand(() => GoToBrowseCollection(collection))));
            }
        }

        public ICommand GoToBrowseCollectionCommand { get; }

        private void GoToBrowseCollection(string collection)
        {
            _mainViewModel.ChangeViewModel(new BrowseCollectionViewModel(_mainViewModel, collection));
        }

        public ICommand AddNewCollectionCommand {  get; }
        private void AddNewCollection()
        {
            DataAccess.AddCollection(NewCollectionName);
            CollectionCards.Add(new BigCardViewModel(NewCollectionName, "Chercher", new RelayCommand(() => GoToBrowseCollection(NewCollectionName))));
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
