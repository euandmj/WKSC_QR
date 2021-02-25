using Core.Encoding;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Text;

namespace GUI
{
    class ZxingQRDecoder<TSource>
        : IQRDecoder<TSource>
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        private TSource Deserialize(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            return JsonConvert.DeserializeObject<TSource>(json);
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
