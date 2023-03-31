using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast.web.ViewModels.Courses
{
    public class CoursePostViewModel
    {
        public Guid CourseId { get; set; }
        // [Required(ErrorMessage = "CourseNumber saknas")]
        // [StringLength(142, MinimumLength = 8)]
        public string CourseNumber {get; set;}
        [Required(ErrorMessage = "CourseTitle saknas")]
        public string CourseTitle {get; set;} = "";
        // [Required(ErrorMessage = "StartDate saknas")]
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        //[Required(ErrorMessage = "EndDate saknas")]
        //public TimeDate EndDate {get; set;}
        // [Required(ErrorMessage = "CourseLenght saknas")]
        public int CourseLenght {get; set;}
        public string Description { get; set; }
    }
}