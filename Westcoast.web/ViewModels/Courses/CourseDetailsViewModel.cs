using Westcoast.web.Models;
using Westcoast.web.ViewModels.Students;
using Westcoast.web.ViewModels.Teachers;
using WestcoastAPI.Models;

namespace Westcoast.web.ViewModels;

    public class CourseDetailsViewModel
    {
        public Guid CourseId {get; set;}
        public string CourseNumber {get; set;}
        public string CourseTitle {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public int CourseLenght {get; set;}
        public string ImageUrl {get; set;}
        public string Description {get; set;}
        public Teacher Teacher {get; set;}
        public ICollection<StudentListViewModel> Students {get; set;}
    }
