using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Core.PDFArranger;
using System.Drawing;
using Core.Models;

namespace Core.Test
{
    [TestFixture]
    class BitmapPageTests
    {

        private Bitmap GetTestBitmap()
        {
            var bmp = new Bitmap(100, 100);
            using var g = Graphics.FromImage(bmp);

            g.FillRectangle(Brushes.Pink, 0, 0, 100, 100);
            
            return bmp;
        }

        private Bitmap GetBitmapFromRegion(Bitmap src, int x, int y, int w, int h)
        {
            var bmp = new Bitmap(x, y);
            var g = Graphics.FromImage(bmp);

            for(int i = x; i < x + w; i++)
            {

            }
            return null;
        }

        private bool AreEqual(Bitmap a, Bitmap b)
        {
            if (a.Equals(b)) return true;
            if (a == b) return true;

            if (a.Width != b.Width) return false;
            if (a.Height != b.Height) return false;

            for(int x = 0; x < a.Width; x++)
            {
                for(int y = 0; y < a.Height; y++)
                {
                    var pxA = a.GetPixel(x, y);
                    var pxB = b.GetPixel(x, y);

                    if (pxA.Equals(pxB)) return false;
                }
            }
            return true;
        }




    }
}
