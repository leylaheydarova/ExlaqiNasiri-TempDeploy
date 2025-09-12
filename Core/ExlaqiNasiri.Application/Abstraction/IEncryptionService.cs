namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IEncryptionService
    {
        string EncryptText(string plainText);
        string DecryptText(string cipherText);
    }
}
