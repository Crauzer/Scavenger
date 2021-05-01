﻿using LeagueToolkit.IO.PropertyBin;
using Microsoft.WindowsAPICodePack.Dialogs;
using Scavenger.MVVM.ViewModels;
using Scavenger.Utilities;
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

namespace Scavenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; private set; } = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this.ViewModel;

            InitializeViewModel();
        }

        public async void InitializeViewModel()
        {
            this.ViewModel.Initialize();

            await this.ViewModel.UpdateHashtables();

            Hashtables.Load();
        }

        private void OnFileOpen(object sender, RoutedEventArgs e)
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filters.Add(new CommonFileDialogFilter("BIN Files", "*.bin"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.ViewModel.LoadBinTree(new BinTree(dialog.FileName));
            }
        }
    }
}
