namespace FamilyApplication.DTOs
{
    public class ForgetPassDTO
    {
        public string Username { get; set; }
        public string Phone { get; set; }
    }

    public class UpdatePasswordDTO
    {
        public int id { get; set; } 
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }

    }
}
