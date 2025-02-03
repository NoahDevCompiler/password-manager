using System.Security.Cryptography;
using System.Text;

namespace password_manager_API.Helpers
{
    public class EncryptionHelper
    {
        public static (string EncryptedText, string IVBase64) EncryptSecret(string plainText, string base64Key) {

            byte[] key = Convert.FromBase64String(base64Key);
            byte[] iv;

            using (Aes aes = Aes.Create()) {
                aes.Key = key;
                aes.GenerateIV();
                iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    string encryptedText = Convert.ToBase64String(ms.ToArray());
                    string ivBase64 = Convert.ToBase64String(iv);

                    return (encryptedText, ivBase64);
                }
            }
        }
        public static string DecryptSecret(string encryptedText, string base64Key, string base64IV) {
            byte[] key = Convert.FromBase64String(base64Key);
            byte[] iv = Convert.FromBase64String(base64IV);
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create()) {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Write)) {
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}
