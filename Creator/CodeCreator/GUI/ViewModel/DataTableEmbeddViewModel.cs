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

namespace GUI.ViewModel
{
    public class DataTableEmbeddViewModel
        : BaseViewModel
    {
        private const string path = @"C:\WKSC_SCNR\storage.csv";


        private string _errorText;
        private ICollection<YardItem> _dataSource;
        private IColumnSchema _columnSchema;
        private readonly IQREncoder<YardItem> _qRSerialiser = new ZXingQREncoder<YardItem>();
        //private readonly FileSystemWatcher _configWatcher = new FileSystemWatcher(AppConfig.)
        //{
        //    NotifyFilter = NotifyFilters.LastWrite,
        //    EnableRaisingEvents = true
        //};

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

            //_configWatcher.Filter = "*.config";
            //_configWatcher.Changed += (s, e) => Init();

            if(AppConfig.Config != null)
            {
                Init();
            }
        }

        public void Init()
        {
            _columnSchema = new YardItemColumnSchema(
                    (YardItemCSVSchema.ZONE_Col, AppConfig.Config.RowColumnn),
                    (YardItemCSVSchema.CLASS_Col, AppConfig.Config.ClassColumn),
                    (YardItemCSVSchema.SAIL_Col, AppConfig.Config.SailColumn),
                    (YardItemCSVSchema.OWNER_Col, AppConfig.Config.OwnerColumn));
            InitDataSource();
        }

        private void InitDataSource()
        {
            if (AppConfig.Config.SpreadsheetFile == null)
            {
                ErrText = "invalid schema or file";
                return;
            }

            using (var adapter = new AdapterCSV<YardItem>(new YardItemCSVSchema(_columnSchema)))
            {
                adapter.ReadCSV(AppConfig.Config.SpreadsheetFile);
                DataSource = adapter.GetItems().ToList();
            }
        }


        private Command OnCreateCommand()
        {
            return new Command((x) =>
            {
                var bmp = _qRSerialiser.Encode(SelectedItem);


                if (bmp != null)
                {
                    var win = new ExportItemsPage(bmp.ToBitmapImage());
                    win.ShowDialog();
                }
            });
        }


        public Command CreateCommand { get => OnCreateCommand(); }


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
        public Visibility ErrorVisibility { get; set; }
        
        public string ErrText
        {
            get => _errorText;
            set
            {
                SetProperty(ref _errorText, value);
            }
        }

    }
}
