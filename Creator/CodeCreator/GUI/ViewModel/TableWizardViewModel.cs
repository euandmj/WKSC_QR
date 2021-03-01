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
        private bool _canSave;
        private string _selectedFile;

        public TableWizardViewModel()
        {

            LoadFileCommand = new Command((x) => OnLoadFile());
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

        
      


        public string SelectedCSVFile
        {
            get => _selectedFile;
            set => SetProperty(ref _selectedFile, value);
        }
        public ICommand LoadFileCommand { get; set; }
        public string RowColumn { get; set; }
        public string ClassColumn { get; set; }
        public string SailNumberColumn { get; set; }
        public string OwnerColumn { get; set; }

    }
}
