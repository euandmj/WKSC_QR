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
using Core;
using System.Windows.Shapes;
using System.Diagnostics;

namespace GUI.View.CellPicker
{
    /// <summary>
    /// Interaction logic for DataGridCellClick.xaml
    /// </summary>
    public partial class DataGridCellClick : UserControl, IDisposable
    {
        private readonly IList<ClickableCell> _innerList = new List<ClickableCell>();
        private bool disposedValue;

        public int Columns { get; set; } = 2;
        public int Rows { get; set; } = 5;

        public int CheckedCount => dg.Children.OfType<ClickableCell>().Where(x => x.IsChecked).Count();
        public int UncheckedCount => (Rows * Columns) - CheckedCount;

        public DataGridCellClick()
        {
            InitializeComponent();

            // Create rows + columns
            dg.ColumnDefinitions.AddRange(Columns);
            dg.RowDefinitions.AddRange(Rows);

            BuildCells();
        }

        private void BuildCells()
        {
            for (int i = 0; i < Columns * Rows; i++)
            {
                int row = i / Columns;
                int col = i % Columns;

                var lbl = new ClickableCell(row + 1, col);

                _innerList.Add(lbl);
                dg.Children.Add(lbl);

                lbl.PreviewMouseLeftButtonDown += this.OnCellClickProxy;
            }
        }


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
            var n = (int)e.NewValue;

            
            for(int i = 0; i < _innerList.Count; i++)
            {
                var ctl = _innerList[i];

                ctl.IsChecked = i < n;
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
                    }
                }
                disposedValue = true;
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
