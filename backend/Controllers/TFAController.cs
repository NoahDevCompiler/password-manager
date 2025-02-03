using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using password_manager_API.Helpers;
using password_manager_API.Models;

namespace password_manager_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TFAController : Controller
    {
        private readonly TwoFactorAuth _twoFactorAuth;
        public TFAController( TwoFactorAuth twoFactorAuth) {
            _twoFactorAuth = twoFactorAuth;
        }
        [HttpPost]
        public IActionResult Post([FromBody] LoginUserDTO _user) {

            DBResult resCheckuser = program.DB.IsUserTaken(_user.Username);
            if (!resCheckuser.Success) {
                return BadRequest(new { Message = $"{resCheckuser.Error}" });
            }

            var (encryptedSecret, ivBase64, qrCodeUrl) = _twoFactorAuth.Setup2FA(_user.Username);
            DBResult resTotpSave = program.DB.SaveUserTotpCredentials(_user.Username, encryptedSecret, ivBase64);

            if (!resTotpSave.Success) {
                return BadRequest(new { Message = $"{resTotpSave.Error}" });
            }

            return Ok(new {
                message = $"{resTotpSave.Message}",
                qrcode = qrCodeUrl
            });
        }
    }
}
