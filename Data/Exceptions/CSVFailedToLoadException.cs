using System;

namespace Data
{
    public class CSVFailedToLoadException
        : Exception
    {


        public CSVFailedToLoadException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
