using System.ComponentModel.DataAnnotations;

namespace WestcoastAPI.Models;

    public class Student : Person
    {
        [Key]
        public Guid StudentId {get; set;}
        // Navigation
        public ICollection<StudentCourse> StudentCourses {get; set;}
}
