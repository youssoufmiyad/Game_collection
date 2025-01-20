using Game_collection.Models;
using Game_collection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_collection.Views
{
    /// <summary>
    /// Logique d'interaction pour AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {

        public AddGame()
        {
            InitializeComponent();
            DataContext = new AddGameViewModel(this);
        }

        public AddGame(Game game)
        {
            InitializeComponent();
            DataContext = new AddGameViewModel(this, game);
        }

        public AddGame(string collectionName)
        {
            InitializeComponent();
            DataContext = new AddGameViewModel(this, collectionName);
        }

        public AddGame(Game game, string collectionName)
        {
            InitializeComponent();
            DataContext = new AddGameViewModel(this, game, collectionName);
        }
    }
}
