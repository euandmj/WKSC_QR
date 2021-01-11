using GUI.ViewModels;
using System;
using Xamarin.Forms;

namespace GUI.Views
{
    public partial class ScanPage : ContentPage
    {
        private readonly ScanViewModel viewModel;

        public ScanPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ScanViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SelectedItem = null;
            zxScanner.IsScanning = true;
        }

        private void QR_FlashButtonClicked(Button sender, EventArgs e)
        {
            zxScanner.ToggleTorch();
        }
    }
}