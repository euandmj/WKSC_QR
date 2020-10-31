using System;
using System.Collections.Generic;
using System.Linq;
using GUI.ViewModels;
using GUI.Views;
using Xamarin.Forms;

namespace GUI
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(RecentScansPage), typeof(RecentScansPage));

        }

        private async void LogoutItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void ImportDb_Clicked(object sender, EventArgs e)
        {

        }
    }
}
