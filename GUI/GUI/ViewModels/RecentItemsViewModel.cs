using Core.Extensions;
using Core.Models;
using GUI.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class RecentItemsViewModel : BaseViewModel
    {
        private YardItem _selectedItem;

        public ObservableCollection<YardItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<YardItem> ItemTapped { get; }

        public RecentItemsViewModel()
        {
            Title = "Recently Scanned";
            Items = new ObservableCollection<YardItem>(DataStore.GetRecentItemsAsync().Result);

            ItemTapped = new Command<YardItem>(OnItemSelected);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                Items.AddRange(await DataStore.GetRecentItemsAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnItemSelected(YardItem item)
        {
            if (item is null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Id)}={item.Id}");
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            Items.Clear();
            Items.AddRange(await DataStore.GetRecentItemsAsync());
        }

        public YardItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public bool IsRefreshEnabled => false;
    }
}