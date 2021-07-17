using GUI.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GUI.View.CellPicker
{
    [DebuggerDisplay("{Row} - {Column}")]
    /// <summary>
    /// Interaction logic for ClickableCell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        public event RoutedEventHandler Checked;
        public event RoutedEventHandler Unchecked;


        private readonly Brush _defbg;

        public bool IsChecked 
        { 
            get => (bool)cb.IsChecked; 
            set
            {
                cb.IsChecked = value;
            }
        }

        public int Row { get; }
        public int Column { get; }


        public Cell(int row, int column)
        {
            InitializeComponent();

            DataContext = new ClickableCellViewModel();

            Grid.SetRow(this, row);
            Grid.SetColumn(this, column);

            _defbg = cb.Background;

            cb.Checked += (s, e) => Checked?.Invoke(s, e);
            cb.Unchecked += (s, e) => Unchecked?.Invoke(s, e);

            Row = row;
            Column = column;
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cb.IsChecked = !cb.IsChecked;
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            cb.Background = Brushes.AliceBlue;
        }

        private void DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            cb.Background = _defbg;
        }

        class ClickableCellViewModel : BaseViewModel
        {

            private bool isChecked = true;

            public bool IsChecked
            {
                get => isChecked;
                set => SetProperty(ref isChecked, value);
            }
        }
    }

    
}
