using GUI.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GUI.Views
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            zxScanner.IsScanning = true;
        }

        private void QR_FlashButtonClicked(Button sender, EventArgs e)
        {
            zxScanner.ToggleTorch();
        }
    }
}