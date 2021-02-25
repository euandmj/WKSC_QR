using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace qrgentest
{
    /// <summary>
    /// .net FWK does not require BarcodeWriter.Renderer
    /// Mobile is .net Std 
    /// .net std zxing 
    /// to put reader and writer into .net std has conflict over zxing.portable and zxing.net
    /// .... at least i think 
    /// </summary>
    class Backup_QREncoder
    {
        private static readonly System.Text.Encoding Encoding = System.Text.Encoding.UTF8;

        private readonly BarcodeWriter<Bitmap> _qrWriter;

        public Backup_QREncoder(int width = 250, int height = 250)
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

            };
        }


        private string Serialize(YardItem item)
        {
            return JsonConvert.SerializeObject(item);
        }

        private YardItem Deserialize(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            return JsonConvert.DeserializeObject<YardItem>(json);
        }
        public Bitmap Encode(YardItem item, BarcodeWriter writer)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            //_qrWriter.Renderer = renderer;
            try
            {
                var json = Serialize(item);
                var bytes = System.Text.Encoding.UTF8.GetBytes(json);
                string b64 = System.Convert.ToBase64String(bytes);
                return writer.Write(b64);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating bitmap from {typeof(YardItem)}", ex);
            }
        }
        public bool TryDecode(string rawString, out YardItem item)
        {
            if (string.IsNullOrEmpty(rawString)) throw new ArgumentNullException(nameof(rawString));

            item = default;

            try
            {
                var b64 = System.Convert.FromBase64String(rawString);
                var json = System.Text.Encoding.UTF8.GetString(b64);
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
