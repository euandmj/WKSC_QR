using Core.Models;
using System;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private string _owner;
        private string _class;
        private char _zoneChar;
        private short _zoneNum;
        private DateTime _paid;
        private DateTime _due;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return 
                !string.IsNullOrWhiteSpace(_owner) &&
                Enum.TryParse<BoatClass>(_class, ignoreCase: true, out _);
        }

        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public string Class
        {
            get => _class;
            set => SetProperty(ref _class, value);
        }

        public string ZoneChar
        {
            get => $"{_zoneChar}";
            set => SetProperty(ref _zoneChar, char.Parse(value));
        }

        public string ZoneNum
        {
            get => $"{_zoneNum}";
            set => SetProperty(ref _zoneNum, short.Parse(value));
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Enum.TryParse<BoatClass>(_class, ignoreCase: true, out var newclass);

            var newItem = new YardItem()
            {
                Zone = $"{ZoneChar}{ZoneNum}",
                Owner = _owner,
                BoatClass = _class
            };

            //await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
