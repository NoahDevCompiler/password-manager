using System.ComponentModel.DataAnnotations;
using password_manager_API.Models.Interfaces;

namespace password_manager_API.Models
{
    public class LoginUserDTO : IUserNameProvider
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string TotpCode { get; set; }
    }
}
