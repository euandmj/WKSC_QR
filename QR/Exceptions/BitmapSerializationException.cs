using System;
using System.Collections.Generic;
using System.Text;

namespace QR.Exceptions
{
    public class BitmapSerializationException
        : Exception
    {


        public BitmapSerializationException(string message, Exception innerExcept)
            : base(message, innerExcept)
        {

        }

    }
}
