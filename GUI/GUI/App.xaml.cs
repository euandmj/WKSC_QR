using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GUI.Views;
using Core.Services;

namespace GUI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<YardItemDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Shell.Current.GoToAsync("//ScanPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
