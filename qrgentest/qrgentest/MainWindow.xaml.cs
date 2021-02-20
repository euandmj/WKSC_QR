using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;

namespace qrgentest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //var options = new QrCodeEncodingOptions
            //{
            //    DisableECI = true,
            //    CharacterSet = "UTF-8",
            //    Width = 250,
            //    Height = 250,
            //};
            //var writer = new BarcodeWriter();
            //writer.Format = BarcodeFormat.QR_CODE;
            //writer.Options = options;



            //var qr = new ZXing.BarcodeWriter();
            //qr.Options = options;
            //qr.Format = ZXing.BarcodeFormat.QR_CODE;
            ////var result = new Bitmap(qr.Write("0a3f673f-4ba4-4458-a266-4a1967f84aa3"));
            //var result = new Bitmap(qr.Write("82253571-c5f4-42f2-81e3-20aaa0f3551c"));

            //fooimage.Source = Convert(result);

            //fooimage2.Source = Convert(new Bitmap(qr.Write("b8ae8d13-fd78-460b-ab83-e26ac0540afe")));
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
