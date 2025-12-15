namespace FamilyApplication.DTOs
{
    public class ShowAllMembersReponseDTO
    {
        public int id {  get; set; }
        public string ProfileImage { get; set; }
        public string FamilyHeadFName { get; set; }
        public string FamilyHeadLName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Role { get; set; }    
        public ICollection<MemberResponseDTO> Members { get; set; }
    }
}
