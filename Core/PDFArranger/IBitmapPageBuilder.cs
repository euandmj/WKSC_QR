using System;
using System.Collections.Generic;

namespace Core.PDFArranger
{
    public interface IBitmapPageBuilder
        : IDisposable
    {

        int Count { get; }
        public void Build();
        public IEnumerable<string> Save(string path); // TODO: set opts for file name?
    }
}