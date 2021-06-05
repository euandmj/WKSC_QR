using Core.Models;
using Data.CSV;
using Data.Models;
using Data.Providers;
using Data.Services;
using GUI.Commands;
using GUI.Configuration;
using GUI.Providers;
using GUI.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GUI.ViewModel
{
    public class DataTableEmbeddViewModel
        : BaseViewModel
    {
        private readonly IProvider<YardItem, string> _dataStore;
        private YardItem _selectedItem;


        public DataTableEmbeddViewModel()
        {
            //DataSource = new List<YardItem>()
            //{
            //    new YardItem(Guid.Parse("0a3f673f-4ba4-4458-a266-4a1967f84aa3")) { Zone = "A2", BoatClass = BoatClass.Falcon.ToString(), Owner = "Bob Jones" },
            //    //new YardItem(Guid.Parse("58de5eac-5d74-49d8-91b0-3fb148c58b39")) { Zone = "B13", BoatClass = BoatClass.Laser, Owner = "Rob Jones", DueDate = DateTime.Now.AddDays(1) },
            //    //new YardItem(Guid.Parse("82253571-c5f4-42f2-81e3-20aaa0f3551c")) { Zone = "C8", BoatClass = BoatClass.GP14, Owner = "Paul Jones" },
            //    //new YardItem(Guid.Parse("b8ae8d13-fd78-460b-ab83-e26ac0540afe")) { Zone = "D2", BoatClass = BoatClass.Falcon, Owner = "Jeff Jones", DueDate = DateTime.Now.AddDays(16) },
            //    //new YardItem() { Zone = "E2", BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) },
            //    //new YardItem() { Zone = "F12", BoatClass = BoatClass.Laser, Owner = "Indiana Jones" }
            //};

            //_dataStore = Core.DependencyInjection.ContainerClass.ResolveType<IDataStore<YardItem, string>>();
            _dataStore = new YardItemProvider(Refresh);

            CreateCommand           = new Command(x => OnCreate(x));
            ExportFlaggedCommand    = new Command(x => OnExportFlagged());
            RefreshCommand          = new Command(_ => Refresh(_dataStore.GetItems()));
            OpenCommand             = new Command(_ => Process.Start(Global.OutputPath));


            Refresh(_dataStore.GetItems());


            //_dataStore.Refreshed += (e, func) =>
            //{
            //    DataSource = new ObservableCollection<YardItem>(func());
            //    OnPropertyChanged(nameof(DataSource));
            //};
        }

        private void Refresh(IEnumerable<YardItem> newItems)
        {
            DataSource = new ObservableCollection<YardItem>(newItems);
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

        private void OnExportFlagged()
        {
            var itemsToExport = DataSource.Where(x => x.Flagged).ToList();

            if(itemsToExport.Count == 0)
            {
                MessageBox.Show($"No flagged items detected. Currently looking in column {AppConfig.Config.FlagColumn}. Is this correct?");
                return;
            }

            var page = new ExportItemsPage(itemsToExport.ToList(), parent);
            page.ShowDialog();
        }


        public ICommand CreateCommand { get; }
        public ICommand ExportFlaggedCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand OpenCommand { get; }

        public YardItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(IsExportEnabled));
            }
        }

        public ICollection<YardItem> DataSource { get; set; }
        //{
        //    get => _dataSource;
        //    set => SetProperty(ref _dataSource, value);
        //}
        public bool IsExportEnabled => SelectedItem != null;
        public string DebugRowText => $"Rows: {DataSource?.Count}";

    }
}
