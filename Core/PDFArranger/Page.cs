using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Core.PDFArranger
{
    class Page
    {
        private const int PER_PAGE = 5; // x2 wide

        private const int pxWidth = 2480;
        private const int pxHeight = 3508;

        private const int mmWIDTH = 210;
        private const int mmHEIGHT = 297;

        private const float rWIDTHmm = 99.1f;
        private const float rHEIGHTmm = 57.3f;

        private readonly Bitmap _host;
        private readonly Graphics _graphics;
        private readonly IEnumerable<Bitmap> _codes;


        public Page(IEnumerable<Bitmap> codes)
        {
            _codes = codes;
            _host = new Bitmap(pxWidth, pxHeight);
            _graphics = Graphics.FromImage(_host);
            _graphics.PageUnit = GraphicsUnit.Millimeter;
        }

        private IEnumerable<(float x, float y)> GetCoords()
        {
            for (float y = 0; y < PER_PAGE; y++)
            {
                float posY = 5.25f + (y * 57.3f);

                float posX1 = 4.9f;
                float posX2 = (mmWIDTH / 2) + 4.9f;

                yield return (posX1, posY);
                yield return (posX2, posY);
            }
        }

        public void Build()
        {
            _graphics.FillRectangle(Brushes.White, 0, 0, mmWIDTH, mmHEIGHT);

            foreach (var ((x, y), bmp) in GetCoords().Zip(_codes, Tuple.Create))
            {
                _graphics.DrawImage(bmp, x, y);
            }
        }

        public void Save(string path)
        {

            _host.Save(path);
        }
    }
}
