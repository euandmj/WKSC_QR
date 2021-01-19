using Android.Widget;
using Core.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace GUI.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        private YardItem _selectedItem;
        private bool _scanning = true;
        private bool _analysing = true;
        private bool _isTorchOn = false;

        public ICommand ScanResultCommand { get; }
        public ICommand ShowFlashCommand { get; }
        public ICommand ClearSwipeCommand { get; }

        public ScanViewModel()
        {
            Title = "Scan";

            ScanResultCommand = new Command(OnScan);
            ShowFlashCommand = new Command(() => IsTorchOn = !IsTorchOn);
            ClearSwipeCommand = new Command(OnClear);
        }

        public bool StarEnabled => _selectedItem != null;
        public Result QRResult { get; set; }

        public bool IsScanning
        {
            get => _scanning;
            set => SetProperty(ref _scanning, value);
        }

        public bool IsAnalysing
        {
            get => _analysing;
            set => SetProperty(ref _analysing, value);
        }

        public bool IsTorchOn
        {
            get => _isTorchOn;
            set => SetProperty(ref _isTorchOn, value);
        }

        public YardItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(StarEnabled));
            }
        }

        private void OnScan()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Guid.TryParse(QRResult.Text, out Guid id))
                {
                    IsScanning = false;
                    IsAnalysing = false;

                    SelectedItem = await DataStore.GetItemAsync(id);
                    DataStore.AddRecentItem(SelectedItem);

                    IsScanning = true;
                    IsAnalysing = true;
                }
            });
        }

        private void OnClear(object param)
        {
            SelectedItem = null;
        }
    }
}