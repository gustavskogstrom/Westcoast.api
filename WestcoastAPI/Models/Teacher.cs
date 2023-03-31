using System.ComponentModel.DataAnnotations;

namespace WestcoastAPI.Models;

    public class Teacher : Person
    {
        [Key]
        public Guid TeacherId {get; set;}
        // Navigation
        // Agregation
        public ICollection<Course> Courses {get; set;}
        public ICollection<Skill> Skills {get; set;}
    }