using System.Drawing;

namespace Core.Encoding
{
    public interface IQREncoder<TSource>
    {
        Bitmap Encode(TSource item);
    }
}
