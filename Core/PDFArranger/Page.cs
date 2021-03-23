using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Core.PDFArranger
{
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
        private readonly IEnumerable<Bitmap> _codes;

        public Page(IEnumerable<Bitmap> codes)
        {
            _codes = codes;
            _graphics = Graphics.FromImage(Bitmap);
            _graphics.PageUnit = GraphicsUnit.Millimeter;
        }


        public Bitmap Bitmap { get; } = new Bitmap(pxWidth, pxHeight);

        public string Name { get; set; }

        private IEnumerable<(float x, float y)> GetCoords()
        {
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

            _graphics.FillRectangle(Brushes.White, 0, 0, mmWIDTH, mmHEIGHT);

            foreach (var ((x, y), bmp) in GetCoords().Zip(_codes, Tuple.Create))
            {
                _graphics.DrawImage(bmp, x, y);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Bitmap.Dispose();
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
