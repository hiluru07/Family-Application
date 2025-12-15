using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyApplication.Models
{
    public class RegisterModels
    {
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty ;
        public string Fname { get; set; } = string.Empty;

        public string Lname { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Cpassword { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string Status {  get; set; } = string.Empty;

        public ICollection<MemberModels> Members { get; set; }

    }
}
