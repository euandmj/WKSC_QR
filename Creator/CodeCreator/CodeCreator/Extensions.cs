using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace CodeCreator
{
    public static class Extensions
    {



        public static BitmapImage ToBitmapImage(this Bitmap src)
        {            
            using MemoryStream ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
