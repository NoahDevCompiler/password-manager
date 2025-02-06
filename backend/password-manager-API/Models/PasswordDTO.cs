using System.ComponentModel.DataAnnotations;

namespace password_manager_API.Models
{
    public class PasswordDTO
    {
        [Required] public string Password { get; set; }
        [Required] public string Service { get; set; }
        [Required] public int Length { get; set; }
        public Array? EmojiSequence { get; set; }

    }
}
