using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Core.PDFArranger
{
    public class PrintableObject
    {
        public string Text { get; set; } = string.Empty;
        public Bitmap Bitmap { get; set; }
    }

    public class Page
        : IDisposable
    {
        private const int PER_PAGE = 5; // x2 wide


        private const int pxWidth = 793;
        private const int pxHeight = 1122;

        private const int mmWIDTH = 210;
        private const int mmHEIGHT = 297;

        private const float rWIDTHmm = 99.1f;
        private const float rHEIGHTmm = 57.3f;

        private bool _built = false;
        private bool disposedValue;

        private readonly Graphics _graphics;
        private readonly Bitmap _bitmap = new Bitmap(pxWidth, pxHeight); 
        private readonly IEnumerable<PrintableObject> _codes;

        public Page(IEnumerable<PrintableObject> codes)
        {
            _codes = codes;
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.PageUnit = GraphicsUnit.Millimeter;
        }


        public Bitmap Bitmap { get => _bitmap; }

        public string Name { get; set; }

        private IEnumerable<(float x, float y)> GetCoords()
        {
            //yield return (62, 64);
            //yield return (1255, 64);
            //yield return (62, 740);
            //yield return (1255, 740);
            //yield return (62, 1420);
            //yield return (1255, 1420);
            //yield return (62, 2094);
            //yield return (1255, 2094);
            //yield return (62, 2772); 
            //yield return (1255, 2772);

            for (float y = 0; y < PER_PAGE; y++)
            {
                float posY = 5.25f + (y * 57.3f);

                float posX1 = 4.9f + 15;
                float posX2 = (mmWIDTH / 2) + 4.9f + 15;

                yield return (posX1, posY);
                yield return (posX2, posY);
            }
        }

        public void Build()
        {
            if (_built) throw new InvalidProgramException("already built");

            _graphics.FillRectangle(Brushes.White, 0, 0, pxWidth, pxHeight);

            // enumerate the coordinates-codes            
            foreach (var ((x, y), item) in GetCoords().Zip(_codes, Tuple.Create))
            {
                _graphics.DrawString(item.Text, SystemFonts.DefaultFont, Brushes.Black, x, y-3);
                _graphics.DrawImage(item.Bitmap, x, y);
            }
        }

        /// <summary>
        /// Draw on QR codes only to specified cells. 
        /// </summary>
        /// <param name="whitelist">contains indices of the flattened grid that will be drawn into</param>
        public void BuildWithWhitelist(ISet<int> whitelist)
        {
            if (_built) throw new InvalidProgramException("already built");

            _graphics.FillRectangle(Brushes.White, 0, 0, mmWIDTH, mmHEIGHT);

            var coords = GetCoords().ToList();

            // enumerate codes-whitelist
            foreach(var (item, wl_i) in _codes.Zip(whitelist, Tuple.Create))
            {
                // get the pixel coordinates of this particular white list indice
                var (x, y) = coords.ElementAt(wl_i);

                _graphics.DrawString(item.Text, SystemFonts.DefaultFont, Brushes.Black, x, y - 3);
                _graphics.DrawImage(item.Bitmap, x, y);
            }
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _bitmap.Dispose();
                    _graphics.Dispose();
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
