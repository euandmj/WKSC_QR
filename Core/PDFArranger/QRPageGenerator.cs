using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Core.PDFArranger
{
    public class QRPageGenerator
    {
        private const int PER_PAGE = 10;

        private ICollection<Page> _pages = new List<Page>();

        public QRPageGenerator(IEnumerable<Bitmap> src)
        {
            foreach(var bucket in src.Batch(PER_PAGE))
            {
                _pages.Add(new Page(bucket));
            }
        }

        public void Build()
        {
            foreach(var page in _pages)
            {
                page.Build();
            }
        }

        public void Save()
        {
            string path = @"c:\testout";

            foreach(var page in _pages)
            {
                page.Save($"{path}-{Guid.NewGuid()}.bmp");
            }
        }
    }
}
