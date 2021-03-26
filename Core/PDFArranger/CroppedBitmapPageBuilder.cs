using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.PDFArranger
{
    public class CroppedBitmapPageBuilder
        : BitmapPageBuilder
    {
        public CroppedBitmapPageBuilder(
            int cropValue,
            IEnumerable<Bitmap> src)
            : base(src.Skip(cropValue))
        {
            _pages.Add(new Page(src.Take(cropValue)));
        }
    }
}
