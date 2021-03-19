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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ColumnRowPicker.xaml
    /// </summary>
    public partial class ColumnRowPicker : ComboBox
    {
        private ColumnRowPickerViewModel vm;

        public ColumnRowPicker()
        {
            InitializeComponent();

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = vm = new ColumnRowPickerViewModel(PickerMode, IsReadOnly);
        }


        public PickerMode PickerMode { get; set; }
    }

    public enum PickerMode
    {
        Column,
        Row
    }

    class ColumnRowPickerViewModel : BaseViewModel
    {

        readonly IEnumerable<string> ColumnPickerOpts = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Select(x => x.ToString());

        public ColumnRowPickerViewModel(PickerMode mode, bool isReadOnly = false)
        {
            if(mode == PickerMode.Column)
            {
                ItemSource = ColumnPickerOpts;
                IsReadOnly = false | isReadOnly;
            }
            else { throw new NotImplementedException(nameof(mode)); }
        }

        public IEnumerable<string> ItemSource { get; private set; }
        public bool IsReadOnly { get; set; }

    }
}
