using Game_collection.Database;
using Game_collection.Models;
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
    class ModifyGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id;
        public int ID
        {
            get { return _id;}
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _genreOptions;
        public ObservableCollection<string> GenreOptions
        {
            get { return _genreOptions; }
            set
            {
                _genreOptions = value;
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
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _collectionOptions;
        public ObservableCollection<string> CollectionOptions
        {
            get { return _collectionOptions; }
            set
            {
                _collectionOptions = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _platform;
        public string Platform
        {
            get { return _platform; }
            set
            {
                _platform = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _releaseDate;
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _acquisitionDate;
        public DateTime? AcquisitionDate
        {
            get { return _acquisitionDate; }
            set
            {
                _acquisitionDate = value;
                OnPropertyChanged();
            }
        }

        private double? _price;
        public double? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private double? _priceResell;
        public double? PriceResell
        {
            get { return _priceResell; }
            set
            {
                _priceResell = value;
                OnPropertyChanged();
            }
        }

        private byte[]? _cover;
        public byte[]? Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged();
            }
        }

        public ModifyGameViewModel()
        {
            ModifyGameCommand = new RelayCommand(ModifyGame);

            GenreOptions = new ObservableCollection<string>
            {
                "Action",
                "Aventure",
                "RPG",
                "FPS"
            };

            List<string> collections = DataAccess.GetCollections();
            CollectionOptions = new ObservableCollection<string>();
            foreach (var collection in collections)
            {
                CollectionOptions.Add(collection);
            }
        }

        public ModifyGameViewModel(Game game)
        {
            ID = game.Id;
            Name = game.Name;
            Genre = game.Genre;
            Description = game.Description;
            Platform = game.Plateform;
            AcquisitionDate = game.AcquisitionDate;
            ReleaseDate = game.ReleaseDate;
            Price = game.Price;
            PriceResell = game.PriceResell;

            Console.WriteLine("modify game page opened");
            ModifyGameCommand = new RelayCommand(ModifyGame);

            GenreOptions = new ObservableCollection<string>
            {
                "Action",
                "Aventure",
                "RPG",
                "FPS"
            };

            List<string> collections = DataAccess.GetCollections();
            CollectionOptions = new ObservableCollection<string>();
            foreach (var collection in collections)
            {
                CollectionOptions.Add(collection);
            }

        }

        public ModifyGameViewModel(string collectionName)
        {
            Collection = collectionName;


            ModifyGameCommand = new RelayCommand(ModifyGame);



            GenreOptions = new ObservableCollection<string>
            {
                "Action",
                "Aventure",
                "RPG",
                "FPS"
            };

            List<string> collections = DataAccess.GetCollections();
            CollectionOptions = new ObservableCollection<string>();
            if (!collections.Contains(collectionName))
            {
                // Ajouter la collectionName si elle n'existe pas
                CollectionOptions.Add(collectionName);
            }
            foreach (var collection in collections)
            {
                CollectionOptions.Add(collection);
            }
        }

        public ModifyGameViewModel(Game game, string collectionName)
        {
            ModifyGameCommand = new RelayCommand(ModifyGame);

            Collection = collectionName;
            ID = game.Id;
            Name = game.Name;
            Genre = game.Genre;
            Description = game.Description;
            Platform = game.Plateform;
            AcquisitionDate = game.AcquisitionDate;
            ReleaseDate = game.ReleaseDate;
            Price = game.Price;
            PriceResell = game.PriceResell;

            GenreOptions = new ObservableCollection<string>
            {
                "Action",
                "Aventure",
                "RPG",
                "FPS"
            };

            List<string> collections = DataAccess.GetCollections();
            CollectionOptions = new ObservableCollection<string>();
            if (!collections.Contains(collectionName))
            {
                // Ajouter la collectionName si elle n'existe pas
                CollectionOptions.Add(collectionName);
            }
            foreach (var collection in collections)
            {
                CollectionOptions.Add(collection);
            }
        }

        public ICommand ModifyGameCommand { get; }
        private void ModifyGame()
        {
            Game game = new(ID, Name, Genre, Description, Platform, ReleaseDate ?? new DateTime(), AcquisitionDate ?? new DateTime(), Price ?? 29.99, PriceResell ?? 29.99, null);
            DataAccess.ModifyGame(game);
        }

        private void ModifyGame(Game game)
        {
            DataAccess.ModifyGame(game);
        }

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
