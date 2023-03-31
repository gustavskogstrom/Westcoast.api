using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastAPI.Data;
using WestcoastAPI.Models;
using WestcoastAPI.ViewModels.Students;

namespace WestcoastAPI.Controllers;

    [ApiController]
    [Route("api/v1/students")]
    public class StudentsController : ControllerBase
    {
        private readonly WestcoastContext _context;
    public StudentsController(WestcoastContext context)
    {
            _context = context;
    }

    [HttpGet("ListAll")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Students
            .Select(s => new
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
                
            })
            .ToListAsync();
            return Ok(result);
        }



        [HttpGet("Details/{StudentId}")]
        public async Task<IActionResult> Details(Guid StudentId)
        {
            var result = await _context.Students
            .Select(s => new
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                SecurityNumber = s.SecurityNumber,
                Phone = s.Phone,
                StreetAdress = s.StreetAdress,
                PostalCode = s.PostalCode,
                Courses = s.StudentCourses.Select(c => new
                    {
                        CourseId = c.CourseId,
                        CourseTitle = c.Course.CourseTitle,
                        Status = ((CourseStatusEnum)c.Status).ToString()
                    }).ToList(),
            })
            .SingleOrDefaultAsync(s => s.StudentId == StudentId);
            return Ok(result);
        }


        /* [HttpPost]
        public async Task<IActionResult> Add(StudentPostViewModel model)
        {
            var exists = await _context.Students.SingleOrDefaultAsync(
                c => c.Email == model.Email && 
                c.Phone == model.Phone
            );
            if(exists is not null) return BadRequest($"Email {model.Email} och telefonnummer {model.Phone} existerar redan.");

            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityNumber = model.SecurityNumber,
                Phone = model.Phone,
                StreetAdress = model.StreetAdress,
                PostalCode = model.PostalCode,
            };

            await _context.Students.AddAsync(student);

            return StatusCode(500, "Internal Server Error");
        } */

        /* public async Task<IActionResult> AddStudent(Guid StudentId, Guid CourseId)
        {
            // Check if the course exists...
            var student = await _context.Students.SingleOrDefaultAsync(c => c.Id == StudentId);
            if (student is null) return NotFound("Kunde inte hitta student");

            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == CourseId);
            if (course is null) return NotFound("Kunde inte hitta student");

            await _context.StudentCourse.AddAsync(StudentCourse);

            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
        } 
        [HttpPatch("addstudent/{courseId}/{studentId}")]

        public async Task<IActionResult> Add(Guid courseId, Guid studentId)
        {
            // Check if the course exists...
            var student = await _context.Students.SingleOrDefaultAsync(c => c.Id == studentId);
            if (student is null) return NotFound("Kunde inte hitta studnet");

            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == courseId);
            if (course is null) return NotFound("Kunde inte hitta course");

            await _context.StudentCourse.AddAsync(studentCourse);

            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal Server Error");
        } */
    }