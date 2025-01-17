using Game_collection.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Select.xaml
    /// </summary>
    public partial class Select : UserControl
    {
        public static readonly DependencyProperty labelProperty =
       DependencyProperty.Register("Label", typeof(string), typeof(Select), new FrameworkPropertyMetadata("label"));

        public static readonly DependencyProperty selectNameProperty =
        DependencyProperty.Register("SelectName", typeof(string), typeof(Select), new FrameworkPropertyMetadata("select"));

        public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register("SelectedValue", typeof(string), typeof(Select),
        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public static readonly DependencyProperty placeholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(Select), new FrameworkPropertyMetadata("placeholder"));

        public static readonly DependencyProperty OptionsProperty =
        DependencyProperty.Register("Options", typeof(IEnumerable<string>), typeof(Select),
        new FrameworkPropertyMetadata(default(IEnumerable<string>)));

        public IEnumerable<string> Options
        {
            get { return (IEnumerable<string>)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }
        public string Label
        {
            get { return (string)GetValue(labelProperty); }
            set { SetValue(labelProperty, value); }
        }

        public string SelectName
        {
            get { return (string)GetValue(selectNameProperty); }
            set { SetValue(selectNameProperty, value); }
        }

        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(placeholderProperty); }
            set { SetValue(placeholderProperty, value); }
        }

        public Select()
        {
            InitializeComponent();
        }

    }
}
