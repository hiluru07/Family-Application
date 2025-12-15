namespace FamilyApplication.DTOs
{
    public class MemberDTO
    {
        public int id { get; set; }
        public IFormFile? MemberImage { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string Relation { get; set; }
    }
    public class MemberCreateModel
    {
        public int UserId { get; set; }

        public List<MemberDTO> Members { get; set; }
    }
}
