namespace Core.Encoding
{
    public interface IQRDecoder<TSource>
    {
        bool TryDecode(string raw, out TSource item);
    }
}
