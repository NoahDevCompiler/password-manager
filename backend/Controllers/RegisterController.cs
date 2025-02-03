using Microsoft.AspNetCore.Mvc;
using password_manager_API.Models;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using Org.BouncyCastle.Security;
using Konscious.Security.Cryptography;
using password_manager_API.Helpers;
using Newtonsoft.Json;

namespace password_manager_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly TokenConfig? _tokenConfig;

        public RegisterController(TokenConfig tokenConfig) {
            _tokenConfig = tokenConfig;
        }
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDTO _user) {

            string passwordhash = HashingHelper.HashingPassword(_user.Password);

            DBResult resCheckuser = program.DB.IsUserTaken(_user.Username);
            if (!resCheckuser.Success) {
                return BadRequest(new { Message = $"{resCheckuser.Error}" });
            }


            DBResult resCreate = program.DB.CreateUser(_user.Username, passwordhash);

            if (!resCreate.Success) {
                return BadRequest(new { Message = $"{resCreate.Error}" });
            }

            string token = _tokenConfig.Create(_user);
            var jsonresult = JsonConvert.SerializeObject(token);
            return Ok(new {
                message = $"{resCreate.Message}",
                token = jsonresult,
            });
        }
    }
}
