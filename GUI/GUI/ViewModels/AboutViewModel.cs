using GUI.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace GUI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private YardItem _selectedItem;
        private bool _scanning = true;
        private bool _analysing = true;

        public AboutViewModel()
        {
            Title = "Scan";

            StarFocusedCommand = new Command(async () => await Task.FromResult(true));
            ScanResultCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (Guid.TryParse(QRResult.Text, out Guid id))
                    {
                        IsScanning = false;
                        IsAnalysing = false;
                        //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Id)}={g}");
                        SelectedItem = await DataStore.GetItemAsync(id);
                    }
                });
            }); 
        }

        public bool StarEnabled => _selectedItem != null;

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

        public Result QRResult { get; set; }
        public ICommand ScanResultCommand { get; }
        public ICommand StarFocusedCommand { get; }
    }
}