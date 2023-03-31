namespace Westcoast.web.ViewModels;

    public class CourseListViewModel
    {
        public Guid CourseId {get; set;}
        public string CourseNumber {get; set;}
        public string CourseTitle {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public int CourseLenght {get; set;}
        public string ImageUrl {get; set;}
        public string Description {get; set;}
    }
    
