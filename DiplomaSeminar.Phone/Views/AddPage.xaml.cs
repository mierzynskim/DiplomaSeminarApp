using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DiplomaSeminar.Phone.Views
{
    public partial class AddPage : PhoneApplicationPage
    {
        private AddViewModel viewModel;
        public AddPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex;
                var id = -1;
                viewModel = ServiceContainer.Resolve<AddViewModel>();
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    id = int.Parse(selectedIndex);

                }

                await viewModel.Init(id);
                DataContext = viewModel;

            }


        }

        private async void SaveButtonOnClick(object sender, EventArgs e)
        {
            await viewModel.ExecuteSavePresentationCommand();

            if (!viewModel.CanNavigate)
                return;
            NavigationService.GoBack();
        }
    }
}