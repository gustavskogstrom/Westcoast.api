using System.ComponentModel.DataAnnotations;

namespace WestcoastAPI.ViewModels.Courses;

    public class CoursePostViewModel
    {
        // [Required(ErrorMessage = "CourseNumber saknas")]
        // [StringLength(142, MinimumLength = 1)]
        public string CourseNumber {get; set;}
        // [Required(ErrorMessage = "CourseTitle saknas")]
        public string CourseTitle {get; set;} = "";
        // [Required(ErrorMessage = "StartDate saknas")]
        public DateTime StartDate {get; set;}
        //[Required(ErrorMessage = "EndDate saknas")]
        public DateTime EndDate {get; set;}
        // [Required(ErrorMessage = "CourseLenght saknas")]
        public int CourseLenght {get; set;}
        public string Description { get; set; }
}
