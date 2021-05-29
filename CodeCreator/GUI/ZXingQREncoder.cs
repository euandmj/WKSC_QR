using Core.Encoding;
using Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace GUI
{
    /// <summary>
    /// Requires a .net framework nuget dependency for ZXing
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public class ZXingQREncoder<TSource>
        : IQREncoder<TSource>
    {
        private static readonly System.Text.Encoding Encoding = System.Text.Encoding.UTF8;

        private readonly BarcodeWriter _qrWriter;

        public ZXingQREncoder(int width = 200, int height = 200)
        {
            _qrWriter = new BarcodeWriter()
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
            return JsonConvert.SerializeObject(item, Formatting.None);
        }

        public Bitmap Encode(TSource item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

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

        public IEnumerable<Bitmap> Encode(IEnumerable<TSource> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                yield return Encode(item);

        }
    }
}
