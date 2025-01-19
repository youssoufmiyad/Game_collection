using Game_collection.Database;
using Game_collection.Models;
using Game_collection.Views;
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
    internal class BrowseGamesViewModel : INotifyPropertyChanged
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
                OnPropertyChanged();
            }
        }

        string _collectionName;
        public string CollectionName
        {
            get { return _collectionName; }
            set
            {
                _collectionName = value;
                OnPropertyChanged();
            }
        }

        public BrowseGamesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;


            List<Game> games = DataAccess.BrowseGames();
            Cards = new ObservableCollection<CardViewModel>();
            foreach (var game in games)
            {
                Cards.Add(new CardViewModel(_mainViewModel, game, game.Id.ToString()));
            }
            OpenAddGameCommand = new RelayCommand(OpenAddGame);

        }
        public ICommand OpenAddGameCommand { get; set; }

        private void OpenAddGame()
        {
            AddGame addGameWindow = new AddGame();
            addGameWindow.ShowDialog();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

