using password_manager_API.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace password_manager_API.Helpers
{
    public class GeneratePasswordHelper
    {
        public static string GeneratePassword(PasswordDTO _password) {

            string plainpw = _password.Password;
            string service = _password.Service;
            int length = _password.Length;
            string[]? emojisequence = _password.EmojiSequence;

            string mergedstring = $"{plainpw + service + string.Join("", emojisequence)}";

            Console.WriteLine(mergedstring);

            string hash = ComputeSHA256(mergedstring);

            return hash.Substring(0, Math.Min(length, hash.Length));

        }
        private static string ComputeSHA256(string input) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
