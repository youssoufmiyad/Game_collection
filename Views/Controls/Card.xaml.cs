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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_collection.Views.Controls
{
    /// <summary>
    /// Logique d'interaction pour Card.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        private void OnCardClick(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is CardViewModel viewModel)
            {
                viewModel.GoToDetailCommand?.Execute(null);
            }
        }
    }
}
