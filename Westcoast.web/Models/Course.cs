using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WestcoastAPI.Models;

    public class Course
    {
        public Guid CourseId {get; set;}
        public string CourseNumber {get; set;}
        public string CourseTitle {get; set;} ="";
        public string CourseName {get; set;} ="";
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public int CourseLenght {get; set;}
        public string ImageUrl {get; set;}
        public string Description {get; set;}
    }
