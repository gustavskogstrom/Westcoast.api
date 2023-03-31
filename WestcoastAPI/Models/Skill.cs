namespace WestcoastAPI.Models
{
    public class Skill
    {
        public Guid Id {get; set;}
        public string SkillName {get; set;}

        public ICollection<Teacher> Teachers {get; set;}
    }
}