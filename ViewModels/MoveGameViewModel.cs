using Game_collection.Database;
using Game_collection.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    class MoveGameViewModel : INotifyPropertyChanged
    {
        public Window moveGameWindow;

        private string _gameID;
        public string GameID
        {
            get { return _gameID; }
            set
            {
                _gameID = value;
                OnPropertyChanged();
            }
        }

        private string _collectionID;
        public string CollectionID
        {
            get { return _collectionID; }
            set
            {
                _collectionID = value;
                OnPropertyChanged();
            }
        }

        private string _collection;
        public string Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                OnPropertyChanged("Collection");
            }
        }
        private ObservableCollection<string> _collectionOptions;
        public ObservableCollection<string> CollectionOptions
        {
            get { return _collectionOptions; }
            set
            {
                _collectionOptions = value;
                OnPropertyChanged("CollectionOptions");
            }
        }


        public MoveGameViewModel(Window moveGameWindow, string gameID, string collectionID)
        {
            this.moveGameWindow = moveGameWindow;
            GameID = gameID;
            CollectionID = collectionID;

            List<string> collections = DataAccess.GetCollections();
            CollectionOptions = new ObservableCollection<string>();
            foreach (var collection in collections)
            {
                CollectionOptions.Add(collection);
            }
            MoveGameCommand = new RelayCommand(MoveGame);

        }

        public ICommand MoveGameCommand { get; set; }
        private void MoveGame()
        {
            DataAccess.MoveGame(GameID, DataAccess.GetCollectionId(Collection), CollectionID);
            moveGameWindow.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
