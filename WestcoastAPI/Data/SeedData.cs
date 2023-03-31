using System.Text.Json;
using WestcoastAPI.Models;

namespace WestcoastAPI.Data;

    public static class SeedData
    {
        public static async Task LoadCoursesData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Courses.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/json/courses.json");
            var courses = JsonSerializer.Deserialize<List<Course>>(json, options);
            
            if(courses is not null && courses.Count > 0)
            {
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }
        }


        public static async Task LoadTeachersData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Teachers.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/json/teachers.json");
            var teachers = JsonSerializer.Deserialize<List<Teacher>>(json, options);
            
            if(teachers is not null && teachers.Count > 0)
            {
                await context.Teachers.AddRangeAsync(teachers);
                await context.SaveChangesAsync();
            }
        }


        public static async Task LoadStudentsData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Students.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/json/students.json");
            var students = JsonSerializer.Deserialize<List<Student>>(json, options);
            
            if(students is not null && students.Count > 0)
            {
                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }

        }
        
        public static async Task LoadStudentCourseData(WestcoastContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.StudentCourse.Any()) return;
            
            var json = System.IO.File.ReadAllText("Data/json/StudentCourse.json");
            var students = JsonSerializer.Deserialize<List<StudentCourse>>(json, options);
            
            if(students is not null && students.Count > 0)
            {
                await context.StudentCourse.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }

        }
}