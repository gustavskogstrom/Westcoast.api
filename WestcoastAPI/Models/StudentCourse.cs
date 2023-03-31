namespace WestcoastAPI.Models
{
    public class StudentCourse
    {
        public CourseStatusEnum Status {get; set;}

        // Navigation
        public Guid CourseId {get; set;}
        public Course Course {get; set;}

        public Guid StudentId {get; set;}
        public Student Student {get; set;}
    }
}