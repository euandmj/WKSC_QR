using Core.Models;
using GUI.Commands;
using GUI.Properties;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for PasteTool.xaml
    /// </summary>
    public partial class PasteTool : UserControl
    {
        private readonly PasteToolViewModel vm;
        public PasteTool()
        {
            InitializeComponent();

            DataContext = vm = new PasteToolViewModel();
        }
    }


    class PasteToolViewModel : BaseViewModel
    {
        private string _owner;
        private string _row;
        private string _class;
        private string _sailNum;

        public PasteToolViewModel()
        {
            PasteCommand = new Command((x) => OnPaste());
            ResetCommand = new Command((x) => OnReset());
            ExportCommand = new Command((x) => OnExport());
        }

        private void OnPaste()
        {
            var clipboard = Clipboard.GetText().Trim();

            var values = clipboard.Split('\t');

            try
            {
                Owner = values[0];
                Row = values[1];
                Class = values[2];
                SailNumber = values[3];
            }
            catch (IndexOutOfRangeException) { }            
        }

        private void OnReset()
        {
            Owner = null;
            Row = null;
            Class = null;
            SailNumber = null;
        }
        
        private void OnExport()
        {
            var yarditem = new YardItem
            {
                Owner = Owner,
                Zone = Row,
                BoatClass = Class,
                SailNumber = SailNumber
            };

            var page = new ExportItemsPage(new[] { yarditem }, parent);
            page.ShowDialog();
        }

        public ImageSource ResetImage
            => Imaging.CreateBitmapSourceFromHBitmap(Resources.delete.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        public ImageSource PasteImage
            => Imaging.CreateBitmapSourceFromHBitmap(Resources.paste.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());


        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public string Row
        {
            get => _row;
            set => SetProperty(ref _row, value);
        }
        public string Class
        {
            get => _class;
            set => SetProperty(ref _class, value);
        }
        public string SailNumber
        {
            get => _sailNum;
            set => SetProperty(ref _sailNum, value);
        }


        public ICommand ResetCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand ExportCommand { get; set; }


    }
}
