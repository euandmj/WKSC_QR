using Core.Encoding;
using Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace GUI
{
    class ZXingQREncoder<TSource>
        : IQREncoder<TSource>
    {
        private static readonly System.Text.Encoding Encoding = System.Text.Encoding.UTF8;

        private readonly BarcodeWriter<Bitmap> _qrWriter;

        public ZXingQREncoder(int width = 250, int height = 250)
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
                Renderer = new BitmapRenderer()

            };
        }

        private string Serialize(TSource item)
        {
            return JsonConvert.SerializeObject(item);
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
            catch (Exception ex)
            {
                throw new BitmapSerializationException($"Error creating bitmap from {typeof(TSource)}", ex);
            }
        }
    }
}
