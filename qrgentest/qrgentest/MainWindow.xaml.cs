using System.Drawing;
using Core;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.QrCode;
using Core.Models;
using System;
using ZXing.Rendering;
using System.Text;
using Newtonsoft.Json;

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




            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250
            };

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            writer.Renderer = new BitmapRenderer();


            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = ZXing.BarcodeFormat.QR_CODE;
            //var result = new Bitmap(qr.Write("0a3f673f-4ba4-4458-a266-4a1967f84aa3"));
            //var result = new Bitmap(qr.Write("82253571-c5f4-42f2-81e3-20aaa0f3551c"));

            //fooimage.Source = Convert(result);

            //fooimage.Source = Convert(new Bitmap(qr.Write("b8ae8d13-fd78-460b-ab83-e26ac0540afe")));

            var item = new YardItem() { Zone = "E2", BoatClass = BoatClass.GP14, Owner = "Obi Wan Jones", DueDate = DateTime.Now.AddDays(1) };


            var enc = new ZXingQREncoder<YardItem>();
            //var bmp = writer.Write("hello world2");
            var bmp = enc.Encode(item);

            fooimage.Source = Convert(bmp);

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

        private string Serialize(YardItem item)
        {
            return JsonConvert.SerializeObject(item);
        }

        private YardItem Deserialize(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            return JsonConvert.DeserializeObject<YardItem>(json);
        }
        public Bitmap Encode(YardItem item, BarcodeWriter writer)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            //_qrWriter.Renderer = renderer;
            try
            {
                var json = Serialize(item);
                var bytes = System.Text.Encoding.UTF8.GetBytes(json);
                string b64 = System.Convert.ToBase64String(bytes);
                return writer.Write(b64);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating bitmap from {typeof(YardItem)}", ex);
            }
        }
        public bool TryDecode(string rawString, out YardItem item)
        {
            if (string.IsNullOrEmpty(rawString)) throw new ArgumentNullException(nameof(rawString));

            item = default;

            try
            {
                var b64 = System.Convert.FromBase64String(rawString);
                var json = System.Text.Encoding.UTF8.GetString(b64);
                item = Deserialize(json);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
