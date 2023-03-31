namespace WestcoastAPI.Models
{
    public class StudentCourse
    {
        public Guid CourseId {get; set;}
        public Course Course {get; set;}

        public Guid StudentId {get; set;}
        public Student Student {get; set;}
    }
}