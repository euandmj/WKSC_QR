using GUI.Models;
using GUI.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private YardItem _selectedItem;

        public ObservableCollection<YardItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<YardItem> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Recent";
            Items = new ObservableCollection<YardItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<YardItem>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                Items.AddRange(await DataStore.GetItemsAsync(true));
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

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnItemSelected(YardItem item)
        {
            if (item is null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Id)}={item.Id}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
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
    }
}