using GUI.Models;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string owner;
        private string boat;
        private string zone;
        private DateTime duedate;
        private bool paid;


        public string Id
        {
            set => LoadItemId(value);
        }

        public string Owner
        {
            get => owner;
            set => SetProperty(ref owner, value);
        }

        public string BoatClass
        {
            get => boat;
            set => SetProperty(ref boat, value);
        }

        public string Zone
        {
            get => zone;
            set => SetProperty(ref zone, value);
        }

        public string DueDate => duedate.ToString("ddMMYYYY");

        public bool HasPaid
        {
            get => paid;
            set => SetProperty(ref paid, value);
        }

        private async void LoadItemId(string id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(Guid.Parse(id));

                Owner = item.Owner;
                BoatClass = item.BoatClass.ToString();
                Zone = item.Zone;
                HasPaid = item.HasPaid;

                duedate = item.DueDate;

                OnPropertyChanged(nameof(DueDate));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
