﻿using Game_collection.Utils;
using Game_collection.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game_collection.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private INotifyPropertyChanged _currentViewModel;

        public INotifyPropertyChanged CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoToHomeCommand { get; }
        public ICommand GoToChooseCollectionCommand{ get; }

        public MainViewModel()
        {
            GoToHomeCommand = new RelayCommand(() => CurrentViewModel = new HomeViewModel(this));
            

            // Initialisation avec "this" pour passer la référence à MainViewModel
            CurrentViewModel = new HomeViewModel(this);
        }

        // Méthode pour changer la vue actuelle
        public void ChangeViewModel(INotifyPropertyChanged newViewModel)
        {
            CurrentViewModel = newViewModel;
        }

        // Méthode pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
