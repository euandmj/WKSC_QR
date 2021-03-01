using System;

namespace Data
{
    public class DataSetParseException
        : Exception
    {



        public DataSetParseException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
