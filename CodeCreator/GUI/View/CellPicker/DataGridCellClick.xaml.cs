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
    public partial class DataGridCellClick : UserControl
    {
        private IList<ClickableCell> _innerList = new List<ClickableCell>();

        public int Columns { get; set; } = 2;
        public int Rows { get; set; } = 5;

        public int CheckedCount => dg.Children.OfType<ClickableCell>().Where(x => x.IsChecked).Count();


        public DataGridCellClick()
        {
            InitializeComponent();

            

            dg.ColumnDefinitions.AddRange(Columns);
            dg.RowDefinitions.AddRange(Rows);


            for(int i = 0; i < Columns * Rows; i++)
            {
                int row = i / Columns;
                int col = i % Columns;
                var lbl = new ClickableCell();
                _innerList.Add(lbl);
                Grid.SetRow(lbl, row + 1);
                Grid.SetColumn(lbl, col);
                dg.Children.Add(lbl);
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
