using password_manager_API.Models;
using BCrypt.Net;

namespace password_manager_API.Helpers
{
    public class GeneratePasswordHelper
    {
        public static string GeneratePassword(PasswordDTO _password) {

            string plainpw = _password.Password;
            string service = _password.Service;
            int length = _password.Length;
            Array? emojisequence = _password.EmojiSequence;

            

        }
    }
}
