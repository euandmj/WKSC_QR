using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GUI.Services;
using GUI.Views;

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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
