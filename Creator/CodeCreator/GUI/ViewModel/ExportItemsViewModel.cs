using Core.Encoding;
using Core.Extensions;
using Core.Models;
using GUI.Commands;
using GUI.Export;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace GUI.ViewModel
{
    class ExportItemsViewModel
        : BaseViewModel
    {
        private readonly ISet<string> _createdPaths;

        private readonly IQREncoder<YardItem> _qrEncoder = new ZXingQREncoder<YardItem>();
        private readonly ICollection<YardItem> _itemsToExport;

        public ExportItemsViewModel(ICollection<YardItem> images)
        {
            _itemsToExport = images ?? throw new ArgumentNullException(nameof(images));

            DbgImgSrc =
                _qrEncoder.Encode(images.First()).ToBitmapImage();

            _createdPaths = new HashSet<string>(_itemsToExport.Count);
            ExportCommand = new Command(x => OnExport());
            PrintCommand = new Command(x => OnPrint());
        }

        public ImageSource DbgImgSrc { get; }

        private void OnExport()
        {
            //if (ExportYardItemUtility.TryGetFolder(out string directory) == CommonFileDialogResult.Cancel)
            //    return;


            _createdPaths.AddRange(
                ExportYardItemUtility.SaveToFile(Global.OutputPath, 
                                                _itemsToExport, 
                                                _qrEncoder));


            OnPropertyChanged(nameof(IsPrintEnabled));
        }

        private void OnPrint()
        {
            ExportYardItemUtility.Print(_createdPaths);
        }


        public string Message => $"Exporting {_itemsToExport.Count} QR codes";
        public string SaveToHelperText => $"saving to {Global.OutputPath}";
        public ICommand ExportCommand { get; }
        public ICommand PrintCommand { get; }

        public bool IsPrintEnabled => _createdPaths.Count > 0;
    }
}
