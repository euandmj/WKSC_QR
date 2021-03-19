using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Core.PDFArranger
{
    public class BitmapPageBuilder
        : IBitmapPageBuilder
    {
        private const int PER_PAGE = 10;

        private readonly string _path;
        private ICollection<Page> _pages = new List<Page>();
        private bool disposedValue;

        public BitmapPageBuilder(IEnumerable<Bitmap> src)
        {
            //_path = path;
            //if (!Directory.Exists(path)) throw new ArgumentException("the path " + path + " does not exist.");

            foreach(var bucket in src.Batch(PER_PAGE))
            {
                _pages.Add(new Page(bucket));
            }
        }

        public int Count => _pages.Count;

        public void Build()
        {
            foreach(var page in _pages)
            {
                page.Build();
            }
        }

        public IEnumerable<string> Save(string path)
        {
            foreach(var page in _pages)
            {
                var bmp = page.Bitmap;
                var filename = Path.Combine(path, $"{Path.GetRandomFileName()}.png");
                bmp.Save(filename, ImageFormat.Png);


                yield return filename;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var page in _pages)
                        page.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
