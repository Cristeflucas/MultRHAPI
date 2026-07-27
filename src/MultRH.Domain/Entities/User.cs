using Microsoft.AspNetCore.Identity;
using MultRH.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultRH.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public DateTime DateOfBirthday { get; set; }
        [Required]
        public string? Cpf { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public bool IsPremium { get; set; }
    }
}
