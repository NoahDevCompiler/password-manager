using Google.Authenticator;
using password_manager_API.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Net.Mime.MediaTypeNames;

namespace password_manager_API.Helpers
{
    public class TwoFactorAuth
    {
        private readonly TwoFactorAuthenticator _authenticator;
        private readonly string _aesKey;

        public TwoFactorAuth(IConfiguration config) {
            _authenticator = new TwoFactorAuthenticator();
            _aesKey = config["EncryptionSettings:AESKey"]!;
        }
       

        public (string Secret, string IV, string QrCodeUrl) Setup2FA(string username) {
           
            var secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            SetupCode setupInfo = _authenticator.GenerateSetupCode(
                "Password-Manager",
                 username, 
                 secretKey, 
                 false, 
                 3
            );
            var (encryptedSecret, ivBase64) = EncryptionHelper.EncryptSecret(secretKey, _aesKey);
            
            return (encryptedSecret, ivBase64, setupInfo.QrCodeSetupImageUrl);
        }

        public bool Verify2FACode(string secret, string iv, string code) {

       
            string decryptedSecret = EncryptionHelper.DecryptSecret(secret, _aesKey, iv);
            Console.WriteLine(secret);
            Console.WriteLine(decryptedSecret);
            return _authenticator.ValidateTwoFactorPIN(decryptedSecret, code);
        }
    }
}
