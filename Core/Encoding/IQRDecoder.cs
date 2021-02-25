using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Encoding
{
    public interface IQRDecoder<TSource>
    {
        bool TryDecode(string raw, out TSource item);

        //public bool TryDecode(string rawString, out TSource item)
        //{
        //    if (string.IsNullOrEmpty(rawString)) throw new ArgumentNullException(nameof(rawString));

        //    item = default;

        //    try
        //    {
        //        var b64 = Convert.FromBase64String(rawString);
        //        var json = Encoding.GetString(b64);
        //        item = Deserialize(json);

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
