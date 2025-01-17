using Game_collection.Database;
using Game_collection.Models;
using Game_collection.Utils;
using Game_collection.Views;
using Game_collection.Views.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    class DetailViewModel : INotifyPropertyChanged
    {
        // Implémentation de l'interface INotifyPropertyChanged pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MainViewModel _mainViewModel;

        public ICommand GoToBrowseCollectionCommand { get; }


        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
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

        DateTime? _releaseDate;
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged("ReleaseDate");
            }
        }

        DateTime? _acquisitionDate;
        public DateTime? AcquisitionDate
        {
            get { return _acquisitionDate; }
            set
            {
                _acquisitionDate = value;
                OnPropertyChanged("AcquisitionDate");
            }
        }

        double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        double _priceResell;
        public double PriceResell
        {
            get { return _priceResell; }
            set
            {
                _priceResell = value;
                OnPropertyChanged("PriceResell");
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

        public DetailViewModel(MainViewModel mainViewModel, Game game)
        {
            _mainViewModel = mainViewModel;
            // Initialisation des propriétés à partir de l'objet Game
            Name = game.Name;
            Description = game.Description;
            Plateform = game.Plateform;
            ReleaseDate = game.ReleaseDate;
            AcquisitionDate = game.AcquisitionDate;
            Price = game.Price;
            PriceResell = game.PriceResell;
            Cover = game.Cover;

            AddGameToWishlistCommand = new RelayCommand(() => AddGameToWishlist(game));
            OpenAddGameCommand = new RelayCommand(() => OpenAddGame(game));

        }

        public ICommand AddGameToWishlistCommand { get; }
        private void AddGameToWishlist(Game game) 
        {
            DataAccess.AddGame(game);
            DataAccess.AddGameToWishlist(game);
        }

        public ICommand OpenAddGameCommand {  get; }
        private void OpenAddGame(Game game)
        {
            AddGame addGameWindow = new AddGame(game);
            addGameWindow.ShowDialog();
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
