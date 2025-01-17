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
    /// Logique d'interaction pour Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public static readonly DependencyProperty labelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(Input), new FrameworkPropertyMetadata("label"));

        public static readonly DependencyProperty inputNameProperty =
        DependencyProperty.Register("InputName", typeof(string), typeof(Input),
        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty placeholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(Input), new FrameworkPropertyMetadata("placeholder"));

        public string Label
        {
            get { return (string)GetValue(labelProperty); }
            set { SetValue(labelProperty, value); }
        }

        public string InputName
        {
            get { return (string)GetValue(inputNameProperty); }
            set { SetValue(inputNameProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(placeholderProperty); }
            set { SetValue(placeholderProperty, value); }
        }


        public Input()
        {
            InitializeComponent();
        }
    }
}
