using Android.Widget;
using Core.Encoding;
using Core.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace GUI.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        private readonly IQRDecoder<YardItem> _qRSerialiser = new ZxingQRDecoder<YardItem>();

        private YardItem _selectedItem;
        private bool _scanning = true;
        private bool _analysing = true;
        private bool _isTorchOn = false;

        public ICommand ScanResultCommand { get; }
        public ICommand ShowFlashCommand { get; }
        public ICommand ClearSwipeCommand { get; }
        public ICommand StarCommand { get; }

        public ScanViewModel()
        {
            Title = "Scan";

            ScanResultCommand = new Command(OnScan);
            ClearSwipeCommand = new Command(OnClear);
            StarCommand = new Command(OnStar);
            ShowFlashCommand = new Command(() => IsTorchOn = !IsTorchOn);
        }

        public bool StarEnabled => _selectedItem != null;
        public string StarText
        {
            get
            {
                if (!StarEnabled) return "";
                return SelectedItem.Starred
                    ? "✰"
                    : "★";
            }
        }
            
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
                OnPropertyChanged(nameof(StarText));
            }
        }

        private void OnScan()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsScanning = false;
                IsAnalysing = false;

                if (_qRSerialiser.TryDecode(QRResult.Text, out YardItem item))
                {
                    SelectedItem = item;
                    // push this scanned item as recently scanned. 
                    DataStore.AddItem(item);
                }
                else { Toast.MakeText(Android.App.Application.Context, "Failed to Decode", ToastLength.Short).Show(); }
                IsScanning = true;
                IsAnalysing = true;
            });
        }

        private void OnClear(object param)
        {
            SelectedItem = null;
        }

        private void OnStar()
        {
            SelectedItem.Starred = !SelectedItem.Starred;
            OnPropertyChanged(nameof(StarText));
        }
    }
}