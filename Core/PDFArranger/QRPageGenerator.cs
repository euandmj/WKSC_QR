using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core
{
    public class QRPageGenerator
    {
        private IReadOnlyCollection<Bitmap[]> bmpPages;
        private const int PER_PAGE = 5;
        private const int pWIDTH = 2480;
        private const int pHEIGHT = 3508;

        private const float rWIDTH = 99.1f;
        private const float rHEIGHT = 57.3f;



        private IReadOnlyCollection<RectangleF> _rects;

        private float MM_PX(float mm)
        {
            const double ratio = 3.7795275591 / 0.2645833333;




            return (float)(mm / ratio);
        }


        private IReadOnlyCollection<(int x, int y)> Points
            = new List<(int, int)>
            {
                //(rWIDTH, 0),       (rHEIGHT, 0),
                //(rWIDTH, Y_),      (rHEIGHT, Y_),
                //(rWIDTH, Y_*2),    (rHEIGHT, Y_*2)
            };


        private IEnumerable<RectangleF> BuildRectangles()
        {
            for(int y = 1; y <= PER_PAGE; y++)
            {
                for(int x = 1; x <= 2; x++)
                {
                    float posX = 4.9f + (rWIDTH * x) + 2;
                    float posY = 5.25f + (rHEIGHT * y);


                    yield return new RectangleF(
                        posX, posY, rWIDTH, rHEIGHT);
                }
            }
        }

        public QRPageGenerator(Bitmap src)
        {

            var host = new Bitmap(pWIDTH, pHEIGHT);
            var g = Graphics.FromImage(host);
            g.PageUnit = GraphicsUnit.Millimeter;

            g.FillRectangle(Brushes.White, 0, 0, pWIDTH, pHEIGHT);


            //_rects = BuildRectangles();




            //for (int i = 0, x = 100; x < src.Width; i++, x++)
            //{
            //    for(int j = 0, y = 100; y < src.Height; j++, y++)
            //    {
            //        host.SetPixel(x, y, src.GetPixel(i, j));
            //    }

            //}

            //foreach(var (pX, pY) in Points)
            //{
            //    g.DrawImage(src, pX, pY);
            //}

            foreach(var rect in BuildRectangles())
            {
                g.DrawImage(src, rect.X, rect.Y);
            }



            // arrange into batches of 6

            // define 6 start points on page

            // iterate over each (point, bitmap) 

            // draw onto host page








            host.Save(@"c:\testout.bmp");


        }


    }
}
