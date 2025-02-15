﻿using Game_collection.Database;
using Game_collection.Models;
using Game_collection.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;


namespace Game_collection.ViewModels
{
    class AddGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Window addGameWindow;

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }
        private ObservableCollection<string> _genreOptions;
        public ObservableCollection<string> GenreOptions
        {
            get { return _genreOptions; }
            set
            {
                _genreOptions = value;
                OnPropertyChanged("GenreOptions");
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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _platform;
        public string Platform
        {
            get { return _platform; }
            set
            {
                _platform = value;
                OnPropertyChanged("Platform");
            }
        }

        private DateTime? _releaseDate;
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged("ReleaseDate");
            }
        }

        private DateTime? _acquisitionDate;
        public DateTime? AcquisitionDate
        {
            get { return _acquisitionDate; }
            set
            {
                _acquisitionDate = value;
                OnPropertyChanged("AcquisitionDate");
            }
        }

        private double? _price;
        public double? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private double? _priceResell;
        public double? PriceResell
        {
            get { return _priceResell; }
            set
            {
                _priceResell = value;
                OnPropertyChanged("PriceResell");
            }
        }

        private byte[]? _cover;
        public byte[]? Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged("Cover");
            }
        }

        public AddGameViewModel(Window window)
        {
            addGameWindow = window;
            AddGameCommand = new RelayCommand(() =>
            {
                if (Collection.Length > 0)
                {
                    AddGameToCollection(Collection);
                }
                else
                {
                    AddGame();
                }
            });
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

        public AddGameViewModel(Window window, Game game)
        {
            addGameWindow = window;

            Name = game.Name;
            Genre = game.Genre;
            Description = game.Description;
            Platform = game.Plateform;
            ReleaseDate = game.ReleaseDate;
            Price = game.Price;
            PriceResell = game.PriceResell;

            Console.WriteLine("add game page opened");
            AddGameCommand = new RelayCommand(() =>
            {
                if (Collection.Length > 0)
                {
                    AddGameToCollection(Collection);
                }
                else
                {
                    AddGame();
                }
            });
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

        public AddGameViewModel(Window window, string collectionName)
        {
            addGameWindow = window;
            Collection = collectionName;

            AddGameCommand = new RelayCommand(() =>
            {
                if (Collection.Length > 0)
                {
                    AddGameToCollection(Collection);
                }
                else
                {
                    AddGame();
                }
            });



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

        public AddGameViewModel(Window window, Game game, string collectionName)
        {
            addGameWindow = window;
            Collection = collectionName;

            Name = game.Name;
            Genre = game.Genre;
            Description = game.Description;
            Platform = game.Plateform;
            ReleaseDate = game.ReleaseDate;
            Price = game.Price;
            PriceResell = game.PriceResell;


        }

        public ICommand AddGameCommand { get; }
        private void AddGame()
        {
            Game game = new(Name, Genre, Description, Platform, ReleaseDate ?? new DateTime(), AcquisitionDate ?? new DateTime(), Price ?? 29.99, PriceResell ?? 29.99, null);
            try
            {
                DataAccess.AddGame(game);
                addGameWindow.Close();
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
            }


        }

        private void AddGameToCollection(string collectionName)
        {
            Game game = new(Name, Genre, Description, Platform, ReleaseDate ?? new DateTime(), AcquisitionDate ?? new DateTime(), Price ?? 29.99, PriceResell ?? 29.99, null);
            try
            {
                DataAccess.AddGame(game);
                DataAccess.AddGameToCollection(Int32.Parse(DataAccess.GetCollectionId(collectionName)), Int32.Parse(DataAccess.GetGameId(Name)));
                addGameWindow.Close();
            }catch (Exception ex)
            {
                ErrorText = ex.Message;
            }



        }
        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
