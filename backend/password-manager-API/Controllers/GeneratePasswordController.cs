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
    public class GeneratePasswordController : ControllerBase
    {
        private readonly TokenConfig? _tokenConfig;
        public GeneratePasswordController(TokenConfig tokenConfig) {
            _tokenConfig = tokenConfig;
        }
        [HttpPost]
        public IActionResult Post([FromBody] PasswordDTO _password) {

            string hashedpassword = GeneratePasswordHelper.GeneratePassword(_password);
            Console.WriteLine(hashedpassword);

            return Ok(hashedpassword);



        }
    }
}