using System.ComponentModel.DataAnnotations;

namespace MultRH.Application.Users.Dtos
{
    public class LoginUserDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
