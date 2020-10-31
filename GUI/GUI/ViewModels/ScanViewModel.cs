using Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
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
        public ICommand ScanResultCommand { get; }

        public ScanViewModel()
        {
            Title = "Scan";

            ScanResultCommand = new Command(OnScan);
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
                    await DataStore.AddRecentItemAsync(SelectedItem);

                    IsScanning = true;
                    IsAnalysing = true;
                }
            });
        }
    }
}