using System;
using System.Collections.Generic;

namespace Core.PDFArranger
{
    public interface IBitmapPageBuilder
        : IDisposable
    {


        public void Build();
        public IEnumerable<string> Save(string path); // TODO: set opts for file name?
    }
}