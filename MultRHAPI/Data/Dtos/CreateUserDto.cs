using System.ComponentModel.DataAnnotations;

namespace MultRHAPI.Data.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Cpf { get; set; }

    }
}
