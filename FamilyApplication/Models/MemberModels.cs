namespace FamilyApplication.Models
{
    public class MemberModels
    {
        public int Id { get; set; }

        public string MemberImage { get; set; } 
        public string Name { get; set; }    
        public int age { get; set; }
        public string Gender { get; set; }
        public string Relation {  get; set; }
        public int RegisterId { get; set; }
        public RegisterModels Register { get; set; }

    }
}
