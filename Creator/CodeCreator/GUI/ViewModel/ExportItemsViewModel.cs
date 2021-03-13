using Core.Encoding;
using Core.Extensions;
using Core.Models;
using Core.PDFArranger;
using GUI.Commands;
using GUI.Export;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
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

            // set debug qr image
            DbgImgSrc =
                _qrEncoder.Encode(images.First()).ToBitmapImage();
            
            _createdPaths = new HashSet<string>(_itemsToExport.Count);
            PrintCommand = new Command(x => OnPrint());
            ExportCommand = new Command(x => OnExport());
            OpenFolderCommand = new Command(x => Process.Start(Global.OutputPath));
        }

        public ImageSource DbgImgSrc { get; }

        private void OnExport()
        {
            int pageCount = 0;
            try
            {
                using (_ = new WaitCursor())
                {
                    foreach (var batch in _itemsToExport.Batch(100))
                    {
                        foreach (var item in _itemsToExport) item.DueDate = DateTime.Now.AddYears(AppConfig.Config.StickerValidDurationMonths);

                        var bitmaps = _qrEncoder.Encode(batch).ToList();
                        using (var pageBuilder = new BitmapPageBuilder(bitmaps))
                        {
                            pageBuilder.Build();

                            _createdPaths.AddRange(pageBuilder.Save(Global.OutputPath));
                            pageCount += pageBuilder.Count;
                        }


                        if (bitmaps != null)
                        {
                            foreach (var bmp in bitmaps)
                                bmp.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed saving qr codes");
            }
            finally
            {
                SaveToHelperText = $"Exported {pageCount} page(s) to {Global.OutputPath}";
            }
            //_createdPaths.AddRange(
            //    ExportYardItemUtility.SaveToFile(
            //        Global.OutputPath,
            //        _itemsToExport,
            //        _qrEncoder));
            OnPropertyChanged(nameof(IsPrintEnabled));
        }

        private void OnPrint()
        {
            ExportYardItemUtility.Print(_createdPaths);
        }


        private string _saveToHelperText;
        public string SaveToHelperText { get => _saveToHelperText; set => SetProperty(ref _saveToHelperText, value); }
        public string Message => $"Exporting {_itemsToExport.Count} QR code(s)";
        public ICommand ExportCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand OpenFolderCommand { get; }

        public bool IsPrintEnabled => _createdPaths.Count > 0;
    }
}
