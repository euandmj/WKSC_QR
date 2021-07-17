using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI
{

    public static class Extensions
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);

        /* https://gist.github.com/nuitsjp/d9d3380a48277958c90c6926c77b616e */
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
            finally 
            { 
                DeleteObject(handle);
            }
        }

        public static void AddRange(
            this ColumnDefinitionCollection @this,
            int num,
            ColumnDefinition template = null)
        {
            if (template is null) template = new ColumnDefinition();
            for (int i = 0; i < num; i++)
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
            if (template is null) template = new RowDefinition();
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
            foreach (var ele in eles)
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
