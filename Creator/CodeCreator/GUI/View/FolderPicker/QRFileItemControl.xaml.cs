using GUI.View.FolderPicker;
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
    /// Interaction logic for QRFileItemControl.xaml
    /// </summary>
    public partial class QRFileItemControl : UserControl, IComparable<QRFileItemModel>
    {
        public QRFileItemModel Context { get; }

        public QRFileItemControl(QRFileItemModel ctx)
        {
            InitializeComponent();

            DataContext = Context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public int CompareTo(QRFileItemModel other)
        {
            return Context.CompareTo(other);
        }
    }
}
