using Microsoft.AspNetCore.Mvc;
using password_manager_API.Helpers;
using password_manager_API.Models;
using Newtonsoft.Json;

namespace password_manager_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase       
    {
        private readonly TokenConfig? _tokenConfig;
        private readonly TwoFactorAuth _twoFactorAuth; 
        public LoginController(TokenConfig tokenConfig, TwoFactorAuth twoFactorAuth) {
            _tokenConfig = tokenConfig;
            _twoFactorAuth = twoFactorAuth;
        }


        [HttpPost]
        public IActionResult LoginUser([FromBody] LoginUserDTO _user) {

            DBResult resCheckuser = program.DB.CheckUsername(_user.Username);
            if (!resCheckuser.Success) {
                return BadRequest(new { error = resCheckuser.Error });
            }

            DBResult resChecklogin = program.DB.CheckLogin(_user.Username, _user.Password);
            DBResult resTotpCheck = program.DB.GetUserTotpCredentials(_user.Username);

            if (!resChecklogin.Success) {
                return BadRequest(new { error = resCheckuser.Error });
            }
            var creds = (TotpCredentialsDto)resTotpCheck.Message!;

            bool totpCheck = _twoFactorAuth.Verify2FACode(creds.Secret, creds.IV, _user.TotpCode);

            if (!totpCheck){
                return BadRequest(new { error = "Incorrect Code" });
            }

            if (!resChecklogin.Success) {
                    return BadRequest(new { error = resChecklogin.Error });
            } 

            string token = _tokenConfig.Create(_user);
            var jsonresult = JsonConvert.SerializeObject(token);
            return Ok(new {
                message = $"{resChecklogin.Message}",
                token = jsonresult
            });
        }
    }
}
