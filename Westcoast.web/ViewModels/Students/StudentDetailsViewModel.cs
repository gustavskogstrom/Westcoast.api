using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestcoastAPI.Models;

namespace Westcoast.web.ViewModels.Students
{
    public class StudentDetailsViewModel
    {
        public Guid StudentId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string SecurityNumber {get; set;}
        public string Phone {get; set;}
        public string StreetAdress {get; set;}
        public string PostalCode {get; set;}
        public ICollection<StudentCourse> StudentCourses {get; set;}
    }
}