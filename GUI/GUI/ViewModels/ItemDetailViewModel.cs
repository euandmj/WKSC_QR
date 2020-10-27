using GUI.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private YardItem _item;


        public string Id
        {
            set => LoadItemId(value);
        }

        public YardItem Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private async void LoadItemId(string id)
        {
            try
            {
                Item = await DataStore.GetItemAsync(Guid.Parse(id));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
