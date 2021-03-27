using Core;
using Core.Encoding;
using Core.Models;
using Core.PDFArranger;
using GUI.Commands;
using GUI.Export;
using GUI.View.CellPicker;
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

        private DateTime _selectedDate;

        public ExportItemsViewModel(ICollection<YardItem> images)
        {
            _itemsToExport = images ?? throw new ArgumentNullException(nameof(images));

            SelectedDate = AppConfig.Config.ValidUntil;


            // set debug qr image
#if DEBUG
            DbgImgSrc =
                _qrEncoder.Encode(images.First()).ToBitmapImage();
#endif
            _createdPaths = new HashSet<string>(_itemsToExport.Count);
            PrintCommand = new Command(x => OnPrint());
            ExportCommand = new Command(x => OnExport());
            OpenFolderCommand = new Command(x => Process.Start(Global.OutputPath));
        }

        public ImageSource DbgImgSrc { get; }

        private int SaveCappedPage(IList<YardItem> itemsCopy)
        {
            /*
             * TODO: 
             * tidy
             * 
             */

            var cutItems = itemsCopy.Take(CheckedCount).ToArray();
            var bmps = _qrEncoder.Encode(cutItems);

            using (var builder = new BitmapPageBuilder(bmps))
            {
                var whiteList = new HashSet<int>(GetWhiteList());

                builder.BuildWithWhitelist(whiteList);

                _createdPaths.AddRange(builder.Save(Global.OutputPath));

                // clean up and remove used items
                foreach (var bmp in bmps) 
                    bmp.Dispose();
                foreach (var item in cutItems)
                    itemsCopy.Remove(item);

                return builder.Count;
            }
        }
        
        private int SavePages(IList<YardItem> items)
        {
            int pageCount = 0;

            foreach (var batch in items.Batch(100))
            {
                //string aggregateFileName = string.Join(",", batch.Select(x => x.ZoneBoat));                        

                var bitmaps = _qrEncoder.Encode(batch);

                using (var pageBuilder = new BitmapPageBuilder(bitmaps))
                {
                    pageBuilder.Build();

                    _createdPaths.AddRange(pageBuilder.Save(Global.OutputPath));

                    pageCount += pageBuilder.Count;
                }

                // cleanup
                if (bitmaps != null)
                {
                    foreach (var bmp in bitmaps)
                        bmp.Dispose();
                }
            }

            return pageCount;
        }

        public Func<IEnumerable<int>> GetWhiteList;


        private void OnExport()
        {
            int savedCount = 0;
            var itemsCopy = _itemsToExport.ToList();

            foreach (var item in itemsCopy)
                item.DueDate = AppConfig.Config.ValidUntil;

            try
            {
                using (_ = new WaitCursor())
                {
                    // check if we need to build a 'special first page'
                    if(CheckedCount != BitmapPageBuilder.PER_PAGE)
                    {
                        savedCount += SaveCappedPage(itemsCopy);
                    }
                    savedCount += SavePages(itemsCopy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed saving qr codes");
            }
            finally
            {
                itemsCopy = null;
                SaveToHelperText = $"Exported {savedCount} page(s) to {Global.OutputPath}";
                OnPropertyChanged(nameof(IsPrintEnabled));
            }
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
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public bool IsPrintEnabled => _createdPaths.Count > 0;

        public IList<ClickableCell> Cells { get; set; }

        public int CheckedCount => Cells.Where(x => x.IsChecked).Count();


    }
}
