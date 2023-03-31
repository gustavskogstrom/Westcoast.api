using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WestcoastAPI.Models;

    public class Course
    {
        [Key]
        //Skrev int här förut.
        public Guid CourseId {get; set;}
        public string CourseNumber {get; set;}
        public string CourseTitle {get; set;} ="";
        public string CourseName {get; set;} ="";
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public int CourseLenght {get; set;}
        public string ImageUrl {get; set;}
        public string Description {get; set;}
        
        // Navigering

        // Aggregation
        public ICollection<StudentCourse> StudentCourses {get; set;}
    
        // Composition
        public Guid? TeacherId { get; set; }
        public Teacher Teacher {get; set;}
    }
