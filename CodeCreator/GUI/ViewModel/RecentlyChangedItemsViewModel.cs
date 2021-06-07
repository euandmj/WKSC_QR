using Core.Models;
using Data.Providers;
using GUI.Commands;
using GUI.View;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GUI.ViewModel
{
    class RecentlyChangedItemsViewModel
        : BaseViewModel
    {
        readonly IProvider<YardItem, string> _dataStore;
        private YardItem _selectedItem;

        public RecentlyChangedItemsViewModel()
        {
            _dataStore = new Providers.RecentlyEditedYardItemProvider(15, (items) => OnNewData(items));
            
            CreateCommand           = new Command(x => OnCreate(x));
            RefreshCommand          = new Command(_ => OnNewData(_dataStore.GetItems()));
            OpenCommand             = new Command(_ => Process.Start(Global.OutputPath));

            DataSource = new ObservableCollection<YardItem>(_dataStore.GetItems());
        }

        public ObservableCollection<YardItem> DataSource { get; set; }
        public ICommand CreateCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand OpenCommand { get; }

        private void OnNewData(IEnumerable<YardItem> items)
        {
            //foreach(var item in items)
            //{
            //    if (DataSource.Contains(item))
            //    {
            //        DataSource
            //    }
            //}
            // TODO: update only those that have changed. 
            DataSource = new ObservableCollection<YardItem>(items);
            OnPropertyChanged(nameof(DataSource));
        }

        private void OnCreate(object x)
        {
            var ll = x as IList;

            if (ll.Count == 0) return;

            var list = ll.Cast<YardItem>();

            var page = new ExportItemsPage(list.ToList(), parent);
            page.ShowDialog();
        }

        public YardItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(IsExportEnabled));
            }
        }


        public bool IsExportEnabled => SelectedItem != null;
    }
}
