using Newtonsoft.Json;
using System;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using QR.Exceptions;
using ZXing.Rendering;

namespace QR
{
    public class QRBase64Encoder<TSource>
        : IQREncoder<TSource>
    {
        private static readonly System.Text.Encoding Encoding = System.Text.Encoding.UTF8;

        private readonly BarcodeWriter<Bitmap> _qrWriter;

        public QRBase64Encoder(int width = 250, int height = 250)
        {
            _qrWriter = new BarcodeWriter<Bitmap>()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = width,
                    Height = height                    
                },
                //Renderer = new ZXing.Rendering.BitmapRenderer()

            };
        }

        private string Serialize(TSource item)
        {
            return JsonConvert.SerializeObject(item);
        }

        private TSource Deserialize(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            return JsonConvert.DeserializeObject<TSource>(json);
        }

        public Bitmap Encode(TSource item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            //_qrWriter.Renderer = renderer;
            try
            {
                var json = Serialize(item);
                var bytes = Encoding.GetBytes(json);
                string b64 = Convert.ToBase64String(bytes);
                return _qrWriter.Write(b64);
            }
            catch(Exception ex)
            {
                throw new BitmapSerializationException($"Error creating bitmap from {typeof(TSource)}", ex);
            }
        }

        public bool TryDecode(string rawString, out TSource item)
        {
            if (string.IsNullOrEmpty(rawString)) throw new ArgumentNullException(nameof(rawString));

            item = default;

            try
            {
                var b64 = Convert.FromBase64String(rawString);
                var json = Encoding.GetString(b64);
                item = Deserialize(json);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
