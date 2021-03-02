using Data.CSV;
using Data.Models;
using GUI.Commands;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Input;

namespace GUI.ViewModel
{
    class TableWizardViewModel
        : BaseViewModel
    {
        private string rowCol = AppConfig.Config.RowColumnn.ToString();
        private string classCol = AppConfig.Config.ClassColumn.ToString();
        private string sailNoCol = AppConfig.Config.SailColumn.ToString();
        private string ownerCol = AppConfig.Config.OwnerColumn.ToString();
        private string _selectedFile = AppConfig.Config.SpreadsheetFile;

        public TableWizardViewModel()
        {

            LoadFileCommand = new Command((x) => OnLoadFile());
            SaveConfigCommand = new Command((x) => OnSaveConfig());
        }

        private void OnLoadFile()
        {
            var f = new OpenFileDialog
            {
                Filter = "Comma-separated values (.csv) | *.csv"
            };
            var result = f.ShowDialog();

            if(result.HasValue && result.Value)
            {
                SelectedCSVFile = f.FileName;
            }
        }

        private void OnSaveConfig()
        {
            AppConfig.SetConfig(new Configuration.Config
            {
                SpreadsheetFile = SelectedCSVFile,
                ClassColumn = ClassColumn,
                OwnerColumn = OwnerColumn,
                RowColumnn = RowColumn,
                SailColumn = SailNumberColumn 
            });

            Configuration.ConfigLoader.Save(AppConfig.Config);

        }

        public ICommand SaveConfigCommand { get; set; }
        public ICommand LoadFileCommand { get; set; }
        public string SelectedCSVFile
        {
            get => _selectedFile;
            set => SetProperty(ref _selectedFile, value);
        }
        public string RowColumn
        {
            get => rowCol;
            set => SetProperty(ref rowCol, value);
        }
        public string ClassColumn
        {
            get => classCol;
            set => SetProperty(ref classCol, value);
        }
        public string SailNumberColumn
        {
            get => sailNoCol;
            set => SetProperty(ref sailNoCol, value);
        }
        public string OwnerColumn
        {
            get => ownerCol;
            set => SetProperty(ref ownerCol, value);
        }

        public bool CanSave
        {
            get => true;
            //{
            //    return RowColumn != null
            //        && ClassColumn != null
            //        && SailNumberColumn != null
            //        && OwnerColumn != null
            //        && !string.IsNullOrWhiteSpace(SelectedCSVFile);
            //}

        }

    }
}
