using Game_collection.Database;
using Game_collection.Models;
using Game_collection.Utils;
using Game_collection.Views;
using Microsoft.VisualBasic;
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
    class BrowseCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private readonly MainViewModel _mainViewModel;

        ObservableCollection<CardViewModel> _cards;

        public ObservableCollection<CardViewModel> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged("Cards");
            }
        }

        string _collectionName;
        public string CollectionName
        {
            get { return _collectionName; }
            set
            {
                _collectionName = value;
                OnPropertyChanged("CollectionName");
            }
        }

        public BrowseCollectionViewModel(MainViewModel mainViewModel, string collection)
        {
            _mainViewModel = mainViewModel;
            CollectionName = collection;

            if (collection != null)
            {
                List<Game> games = DataAccess.BrowseGamesOfCollections(collection);
                Cards = new ObservableCollection<CardViewModel>();
                foreach (var game in games)
                {
                    Cards.Add(new CardViewModel(_mainViewModel, game, DataAccess.GetGameId(game.Name)));
                }


            }
            else
            {
                Game testGame = new Game("test nom", "test genre", "test description", "test plateform", DateTime.Now, DateTime.Now, 0, 0, null);
                Cards = new ObservableCollection<CardViewModel>
            {
                new CardViewModel(_mainViewModel, testGame),
                new CardViewModel(_mainViewModel, testGame),
                new CardViewModel(_mainViewModel, testGame),
                new CardViewModel(_mainViewModel, testGame),
                new CardViewModel(_mainViewModel, testGame),
                new CardViewModel(_mainViewModel, testGame),
            };
            }


            OpenAddGameCommand = new RelayCommand(OpenAddGame);
        }
        public ICommand OpenAddGameCommand { get; set; }

        private void OpenAddGame()
        {
            AddGame addGameWindow = new AddGame(CollectionName);
            addGameWindow.Closed += new EventHandler(RefreshCards);
            addGameWindow.ShowDialog();
            
        }

        private void RefreshCards(object sender, EventArgs e)
        {
            List<Game> games = DataAccess.BrowseGamesOfCollections(CollectionName);
            Cards = new ObservableCollection<CardViewModel>();
            foreach (var game in games)
            {
                Cards.Add(new CardViewModel(_mainViewModel, game, DataAccess.GetGameId(game.Name)));
            }
            OnPropertyChanged("Cards");
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
