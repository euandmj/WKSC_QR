using GUI.Commands;
using GUI.View;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Encoding;
using System.Windows.Media;
using Data;
using Data.Models;
using System.Linq;
using Data.CSV;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using GUI.Configuration;
using System.Collections;
using System.Windows.Input;
using System.Diagnostics;

namespace GUI.ViewModel
{
    public class DataTableEmbeddViewModel
        : BaseViewModel
    {
        private ICollection<YardItem> _dataSource;
        private IColumnSchema _columnSchema;

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

            CreateCommand = new Command(x => OnCreate(x));
            ExportFlaggedCommand = new Command(x => OnExportFlagged());
            RefreshCommand = new Command(_ => Init(AppConfig.Config));
            OpenCommand = new Command(_ => Process.Start(Global.OutputPath));

            Init(AppConfig.Config);

            AppConfig.ConfigChanged += (s, e) =>
            {
                Init(AppConfig.Config);
            };
        }

        private void Init(Config cfg)
        {
            if (cfg == null) return;
            try
            {
                _columnSchema = cfg.Schema;
                InitDataSource();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed Loading Data Table.", MessageBoxButton.OK);
            }
        }

        private void InitDataSource()
        {
            if (AppConfig.Config.SpreadsheetFile == null)
            {
                return;
            }

            using (var adapter = new AdapterCSV<YardItem>(new YardItemCSVSchema(_columnSchema)))
            {
                adapter.ReadCSV(AppConfig.Config.SpreadsheetFile);
                DataSource = adapter.GetItems().ToList();
                SelectedItem = null;
                OnPropertyChanged(nameof(DebugRowText));
            }
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

        public ICollection<YardItem> DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource, value);
        }
        public bool IsExportEnabled => SelectedItem != null;
        public string DebugRowText => $"Rows: {DataSource?.Count}";

    }
}
