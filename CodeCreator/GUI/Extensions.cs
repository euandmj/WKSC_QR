using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI
{

    public static class Extensions
    {
        /* https://gist.github.com/nuitsjp/d9d3380a48277958c90c6926c77b616e */
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ToBitmapImage(this Bitmap src)
        {
            var handle = src.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, 
                                                            IntPtr.Zero, 
                                                            Int32Rect.Empty, 
                                                            BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
