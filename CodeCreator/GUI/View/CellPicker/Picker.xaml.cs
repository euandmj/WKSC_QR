using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.View.CellPicker
{
    /// <summary>
    /// Interaction logic for DataGridCellClick.xaml
    /// 
    /// The Parent control which holds the datagrid with cells populated with 
    /// <see cref="Cell"/> referenced with <see cref="_cellCollection"/>
    /// 
    /// 
    /// 
    /// </summary>
    public partial class Picker : UserControl, IDisposable
    {
        private bool disposedValue;

        private readonly IList<Cell> _cellCollection;

        public int Columns { get; set; } = 2;
        public int Rows { get; set; } = 5;
        public int CheckedCount { get; private set; }

        public Picker()
        {
            InitializeComponent();

            _cellCollection = new List<Cell>(Columns * Rows);

            // Create rows + columns
            dg.ColumnDefinitions.AddRange(Columns);
            dg.RowDefinitions.AddRange(Rows);

            BuildCells();
        }

        public IList<Cell> Cells => _cellCollection;


        private void BuildCells()
        {
            for (int i = 0; i < Columns * Rows; i++)
            {
                int row = i / Columns;
                int col = i % Columns;

                var lbl = new Cell(row + 1, col);

                _cellCollection.Add(lbl);
                dg.Children.Add(lbl);


                lbl.PreviewMouseLeftButtonDown += this.OnCellClickProxy;
                lbl.Checked += OnChecked;
                lbl.Unchecked += OnUnchecked;
            }
        }

        private void OnChecked(object sender, RoutedEventArgs e) => CheckedCount++;

        private void OnUnchecked(object sender, RoutedEventArgs e) => CheckedCount--;


        private void OnCellClickProxy(object sender, MouseButtonEventArgs e)
        {
            if (sender is Cell cell)
            {
                // if theres only one checked cell block the event args to
                // stop it being unticked
                if (cell.IsChecked && CheckedCount == 1)
                {
                    e.Handled = true;
                }
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_cellCollection is null) return;

            var n = (int)e.NewValue;

            for (int i = 0; i < _cellCollection.Count; i++)
            {
                var ctl = _cellCollection[i];

                if (ctl.IsEnabled)
                {
                    ctl.IsChecked = i < n;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var ctl in dg.Children.OfType<Cell>())
                    {
                        ctl.PreviewMouseLeftButtonDown -= this.OnCellClickProxy;
                        ctl.Unchecked -= this.OnUnchecked;
                        ctl.Checked -= this.OnChecked;
                    }
                }
                disposedValue = true;
            }
        }

        public IEnumerable<int> GetWhiteList()
        {
            for (int i = 0; i < _cellCollection.Count; i++)
            {
                if (_cellCollection[i].IsChecked)
                    yield return i;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }


    
}
