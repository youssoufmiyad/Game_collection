﻿using Game_collection.Models;
using Game_collection.ViewModels;
using System;
using System.Collections;
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
    /// Logique d'interaction pour Modify.xaml
    /// </summary>
    public partial class ModifyGame
    {
        public ModifyGame()
        {
            InitializeComponent();
            DataContext = new ModifyGameViewModel();
        }

        public ModifyGame(Game game, string collection)
        {
            InitializeComponent();
            DataContext = new ModifyGameViewModel(game, collection);
        }
    }
}
