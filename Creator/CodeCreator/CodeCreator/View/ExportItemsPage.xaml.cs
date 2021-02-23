using System.Drawing;
using System.Windows;

namespace CodeCreator.View
{
    /// <summary>
    /// Interaction logic for ExportItemsPage.xaml
    /// </summary>
    public partial class ExportItemsPage : Window
    {
        public ExportItemsPage(Bitmap img)
        {
            InitializeComponent();

            bmp.Source = img.ToBitmapImage();
        }
    }
}
