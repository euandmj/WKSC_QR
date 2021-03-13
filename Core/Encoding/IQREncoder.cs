using System.Collections.Generic;
using System.Drawing;

namespace Core.Encoding
{
    public interface IQREncoder<TSource>
    {
        Bitmap Encode(TSource item);
        IEnumerable<Bitmap> Encode(IEnumerable<TSource> items);
    }
}
