using Core.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Certificates
{
    public class YardItemDecrypter
    {
        private static string filePath;
        private static readonly Lazy<YardItemDecrypter> certService = new Lazy<YardItemDecrypter>(() => new YardItemDecrypter());
        private readonly Lazy<X509Certificate2> _certificate = new Lazy<X509Certificate2>(() => new X509Certificate2(filePath, "123"));


        private YardItemDecrypter() { }

        private YardItem Deserialise(string json)
        {
            return JsonConvert.DeserializeObject<YardItem>(json);
        }

        public void CopyToCache(Stream assetStream, string path)
        {
            if (assetStream == null) throw new ArgumentNullException(nameof(assetStream));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            
            filePath = path;

            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            assetStream.CopyTo(fs);
        }

        public YardItem Decrypt(string str)
        {
            try
            {

                var b64 = Convert.FromBase64String(str);

                var json = Encoding.UTF8.GetString(b64);

                return Deserialise(json);


                //using (var prov = (RSACng)_certificate.Value.GetRSAPrivateKey())
                //{
                //    var bout = prov.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
                //    var text = Encoding.UTF8.GetString(bout);

                //    return Deserialise(text);
                //}
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static YardItemDecrypter Instance => certService.Value;
    }
}
