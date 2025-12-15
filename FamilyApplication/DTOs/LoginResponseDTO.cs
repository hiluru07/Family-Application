namespace FamilyApplication.DTOs
{
    public class LoginResponseDTO
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string ProfileImage { get; set; }
        public string Fname { get; set; }   
        public string Lname { get; set; }
        public string Phone { get; set; }   
        public string Email { get; set; }
        public string Role {  get; set; }
        public string status { get; set; }
        public string Message { get; set; } 
    }
}
