using ExlaqiNasiri.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ExlaqiNasiri.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        readonly byte[] _key;
        readonly byte[] _iv;

        public EncryptionService(IConfiguration configuration)
        {
            _key = Encoding.UTF8.GetBytes(configuration["Aes:Key"]);
            _iv = Encoding.UTF8.GetBytes(configuration["Aes:Iv"]);
        }

        public string DecryptText(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            var buffer = Convert.FromBase64String(cipherText);

            using var decryptor = aes.CreateDecryptor(_key, _iv);
            using var ms = new MemoryStream(buffer);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }

        public string EncryptText(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor(_key, _iv);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
