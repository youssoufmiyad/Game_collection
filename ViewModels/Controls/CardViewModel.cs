using Game_collection.Database;
using Game_collection.Models;
using Game_collection.Utils;
using Game_collection.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly MainViewModel _mainViewModel;

        Game Game { get; set; }

        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        string _plateform;
        public string Plateform
        {
            get { return _plateform; }
            set
            {
                _plateform = value;
                OnPropertyChanged("Plateform");
            }
        }

        string _cover;
        public string Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged("Cover");
            }
        }

        string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        string _visibility;
        public string Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public ICommand GoToDetailCommand { get; }

        //public CardViewModel(MainViewModel mainViewModel, string Title, string Year = "Inconnue", string Price = "Inconnue", string Plateform = "Inconnue", string Cover = "https://picsum.photos/333/147")
        public CardViewModel(MainViewModel mainViewModel, Game game)
        {
            Game = game;
            _visibility = "Visible";
            _mainViewModel = mainViewModel;
            _title = game.Name;
            _year = game.ReleaseDate.ToString("dd/MM/yyyy");
            _price = game.Price.ToString();
            _plateform = game.Plateform;
            _cover = game.Cover;
            GoToDetailCommand = new RelayCommand(GoToDetail);

            OpenAddGameCommand = new RelayCommand(OpenAddGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
            OpenModifyGameCommand = new RelayCommand(OpenModifyGame); 
        }
        public CardViewModel(MainViewModel mainViewModel, Game game, string id)
        {
            Game = game;
            _visibility = "Visible";
            ID = id;
            _mainViewModel = mainViewModel;
            _title = game.Name;
            _year = game.ReleaseDate.ToString("dd/MM/yyyy");
            _price = game.Price.ToString();
            _plateform = game.Plateform;
            _cover = game.Cover;
            GoToDetailCommand = new RelayCommand(GoToDetail);

            OpenAddGameCommand = new RelayCommand(OpenAddGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
            OpenModifyGameCommand = new RelayCommand(OpenModifyGame);
        }

        private void GoToDetail()
        {

            _mainViewModel.ChangeViewModel(new DetailViewModel(_mainViewModel, Game));
        }

        public ICommand OpenAddGameCommand { get; set; }

        private void OpenAddGame()
        {
            AddGame addGameWindow = new AddGame();
            addGameWindow.ShowDialog();
        }

        public ICommand OpenModifyGameCommand { get; set; }
        private void OpenModifyGame()
        {
            AddGame modifyGameWindow = new AddGame(Game, Game.Collection);
            modifyGameWindow.ShowDialog();
        }

        public ICommand DeleteGameCommand { get; set; }
        private void DeleteGame()
        {
            DataAccess.DeleteGameById(Convert.ToInt32(ID));
            _visibility = "Collapsed";
        }

        public ICommand MoveGameCommand { get; set; }
        private void MoveGame()
        {
            DataAccess.MoveGame();
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
