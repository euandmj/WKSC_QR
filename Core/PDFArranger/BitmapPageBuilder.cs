using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Core.PDFArranger
{
    public class BitmapPageBuilder
        : IBitmapPageBuilder
    {
        public const int PER_PAGE = 10;

        private bool disposedValue;
        protected ICollection<Page> _pages = new List<Page>();

        public BitmapPageBuilder(IEnumerable<PrintableObject> src)
        {
            foreach(var bucket in src.Batch(PER_PAGE))
            {
                _pages.Add(new Page(bucket));
            }
        }


        public int Count => _pages.Count;
        public string FileExtension { get; set; } = ".png";


        public void Build()
        {
            foreach(var page in _pages)
            {
                page.Build();
            }
        }

        public void BuildWithWhitelist(ISet<int> whitelist)
        {
            var firstPage = _pages.FirstOrDefault();

            if (firstPage is null) throw new InvalidProgramException("no first page built.");

            firstPage.BuildWithWhitelist(whitelist);
        }


        public IEnumerable<string> Save(string path)
        {
            foreach(var page in _pages)
            {
                var bmp = page.Bitmap;
                var filename = Path.Combine(path, $"{Path.GetRandomFileName()}{FileExtension}");
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
