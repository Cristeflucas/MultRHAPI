using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MultRHAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public DateTime DateOfBirthday { get; set; }
        [Required]
        public string? Cpf { get; set; }
    }
}
