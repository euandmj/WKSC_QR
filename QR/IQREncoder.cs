using System.Drawing;

namespace QR
{
    public interface IQREncoder<T>
    {
        Bitmap Encode(T item);
        bool TryDecode(string raw, out T item);
    }
}
