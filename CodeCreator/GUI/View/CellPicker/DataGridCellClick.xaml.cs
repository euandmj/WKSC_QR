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
    /// <see cref="ClickableCell"/> referenced with <see cref="CellCollection"/>
    /// 
    /// 
    /// 
    /// </summary>
    public partial class DataGridCellClick : UserControl, IDisposable
    {
        private bool disposedValue;

        private readonly IList<ClickableCell> _cellCollection;

        public int Columns { get; set; } = 2;
        public int Rows { get; set; } = 5;       

        public int CheckedCount { get; set; }

        public DataGridCellClick()
        {
            InitializeComponent();

            _cellCollection = new List<ClickableCell>(Columns * Rows);

            // Create rows + columns
            dg.ColumnDefinitions.AddRange(Columns);
            dg.RowDefinitions.AddRange(Rows);

            BuildCells();
        }

        public IList<ClickableCell> Cells => _cellCollection;


        private void BuildCells()
        {
            for (int i = 0; i < Columns * Rows; i++)
            {
                int row = i / Columns;
                int col = i % Columns;

                var lbl = new ClickableCell(row + 1, col);

                _cellCollection.Add(lbl);
                dg.Children.Add(lbl);


                lbl.PreviewMouseLeftButtonDown += this.OnCellClickProxy;
                lbl.Checked += OnChecked;
                lbl.Unchecked += OnUnchecked;
            }
        }

        private void OnChecked(object sender, RoutedEventArgs e)
            => CheckedCount++;

        private void OnUnchecked(object sender, RoutedEventArgs e)
            => CheckedCount--;


        private void OnCellClickProxy(object sender, MouseButtonEventArgs e)
        {
            if(sender is ClickableCell cell)
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

            for(int i = 0; i < _cellCollection.Count; i++)
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
                    foreach(var ctl in dg.Children.OfType<ClickableCell>())
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
            for(int i = 0; i < _cellCollection.Count; i++)
            {
                if(_cellCollection[i].IsChecked)
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


    public static class DataGridExtensions
    {
        public static void AddRange(
            this ColumnDefinitionCollection @this,
            int num,
            ColumnDefinition template = null)
        {
            if(template is null) template = new ColumnDefinition();
            for(int i = 0; i < num; i++)
            {
                @this.Add(new ColumnDefinition()
                {
                    Width = template.Width,
                    MaxWidth = template.MaxWidth,
                    MinWidth = template.MinWidth
                });
            }
        }

        public static void AddRange(
            this RowDefinitionCollection @this,
            int num,
            RowDefinition template = null)
        {
            if(template is null) template = new RowDefinition();
            for (int i = 0; i < num; i++)
            {
                @this.Add(new RowDefinition()
                {
                    Height = template.Height,
                    MaxHeight = template.MaxHeight,
                    MinHeight = template.MinHeight
                });
            }
        }

        public static void AddRange(
            this UIElementCollection @this, 
            IEnumerable<UIElement> eles)
        {
            foreach(var ele in eles)
            {
                @this.Add(ele);
            }
        }

        public static IEnumerable<T> FindAll<T>(this UIElementCollection @this, Func<T, bool> func)
        {
            foreach (var child in @this)
            {
                if (child.GetType() == typeof(T))
                {
                    if (func((T)child)) yield return (T)child;
                }
            }
        }
    }
}
