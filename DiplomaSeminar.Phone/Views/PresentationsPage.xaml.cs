﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.Model;
using DiplomaSeminar.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DiplomaSeminar.Phone.Views
{
    public partial class PresentationsPage : PhoneApplicationPage
    {
        private readonly PresentationsViewModel viewModel;
        private bool loaded;
        public PresentationsPage()
        {
            InitializeComponent();

            viewModel = ServiceContainer.Resolve<PresentationsViewModel>();
            DataContext = viewModel;
            Loaded += OnLoaded;
        }

        private void LongListSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
                return;

            if (viewModel.IsBusy)
                return;

            NavigationService.Navigate(new Uri("/Views/AddPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as Presentation).Id, UriKind.Relative));

            MainLongListSelector.SelectedItem = null;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (loaded)
                return;
            loaded = true;

            await viewModel.ExecuteSyncPresentationsCommand();
        }

        private void RefreshButtonOnClick(object sender, EventArgs e)
        {
            viewModel.SyncPresentationsCommand.Execute(null);
        }

        private void NewPresentationOnClick(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                return;

            NavigationService.Navigate(new Uri("/Views/AddPage.xaml", UriKind.Relative));

        }
    }
}