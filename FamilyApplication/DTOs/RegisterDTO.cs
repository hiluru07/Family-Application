
using System.ComponentModel.DataAnnotations;

namespace FamilyApplication.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public IFormFile? ProfileImage { get; set; }    
        [Required]
        public string Fname { get; set; } = string.Empty;

        [Required]
        public string Lname { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

       
        public string? Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Cpassword { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
    // For profile update
    public class UpdateProfileDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }
        [Required]
        public string Fname { get; set; } = string.Empty;
        [Required]
        public string Lname { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

}

