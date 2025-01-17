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
    public class BigCardViewModel : INotifyPropertyChanged
    {
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

        string _button;
        public string Button
        {
            get { return _button; }
            set
            {
                _button = value;
                OnPropertyChanged("Button");
            }
        }

        public ICommand Command { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        // Méthode pour notifier les changements de propriétés
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BigCardViewModel(string title, string button, ICommand command)
        {
            Title = title;
            Button = button;
            Command = command;
        }
    }
}
