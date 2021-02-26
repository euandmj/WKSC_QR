using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ExportItemsPage.xaml
    /// </summary>
    public partial class ExportItemsPage : Window
    {
        public ExportItemsPage(ImageSource img)
        {
            InitializeComponent();

            bmp.Source = img;
        }
    }
}
