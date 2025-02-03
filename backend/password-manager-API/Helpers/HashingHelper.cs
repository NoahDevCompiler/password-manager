using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace password_manager_API.Helpers
{
    public class HashingHelper
    {
        public static string HashingPassword(string password) {
         
            string hash = BCrypt.Net.BCrypt.HashPassword(password, 13);
            return hash;
        }
        public static bool VerifyPassword(string password, string hash) {

            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
            
    }
}
