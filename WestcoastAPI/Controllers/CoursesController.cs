using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastAPI.Data;
using WestcoastAPI.Models;
using WestcoastAPI.ViewModels.Courses;

namespace WestcoastAPI.Controllers;

    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly WestcoastContext _context;
    public CoursesController(WestcoastContext context)
    {
            _context = context;
    }
        /// <summary>
        /// Listar alla kurser i systemet.
        /// </summary>
    [HttpGet()]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Courses
            .Select(c => new
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                StartDate = c.StartDate,
                CourseLenght = c.CourseLenght,
                // != null ? är en if else bara en shot cut
                Teacher = c.Teacher != null ? c.Teacher.FirstName + " " + c.Teacher.LastName : "Saknas",
                Students = c.StudentCourses.Select(s => new 
                {
                    StudentId = s.StudentId,
                    Name = s.Student.FirstName + " " + s.Student.LastName
                }).ToList()
            })
            .ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// Hämtar en kurs baserat på kurs id(courseId).
        /// </summary>
        /// <returns>En kurs med information och lärare och studenter</returns>
        /// <response code="200">Returnerar en kurs med information</response>

        [HttpGet("Details/{CourseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(Guid CourseId)
        {
            var result = await _context.Courses
            .Select(c => new
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                StartDate = c.StartDate.ToShortDateString(),
                EndDate = c.EndDate.ToShortDateString(),
                Teacher = c.Teacher != null ? new {
                    Id = c.Teacher.TeacherId, 
                    FirstName = c.Teacher.FirstName,
                    LastName = c.Teacher.LastName,
                    Email = c.Teacher.Email,
                    Phone = c.Teacher.Phone
                } : null, 
                Students = c.StudentCourses.Select(s => new{
                    Id = s.StudentId,
                    FirstName = s.Student.FirstName,
                    LastName = s.Student.LastName,
                    Email = s.Student.Email,
                    Phone = s.Student.Phone,
                    Status = ((CourseStatusEnum)s.Status).ToString()
                })
            })
            .SingleOrDefaultAsync(c => c.CourseId == CourseId);

            return Ok(result);
        }


        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CoursePostViewModel model)
        {
            var exists = await _context.Courses.SingleOrDefaultAsync(
                c => c.CourseNumber == model.CourseNumber && 
                c.StartDate == model.StartDate
            );
            if(exists is not null) return BadRequest($"kursnummer {model.CourseNumber} och kurs start {model.StartDate.ToShortDateString()} existerar redan.");

            var createC = new Course{
                
                CourseId = Guid.NewGuid(),
                CourseTitle = model.CourseTitle,
                CourseLenght = model.CourseLenght,
                StartDate = model.StartDate,
                EndDate = model.StartDate.AddDays(model.CourseLenght * 7),
                Description = model.Description
            };
            await _context.Courses.AddAsync(createC);

            if(await _context.SaveChangesAsync() > 0){
                var result = new{
                    CourseId = createC.CourseId,
                    CourseTitle = createC.CourseTitle,
                    DateTime = createC.StartDate.ToShortDateString(),
                    Endate = createC.EndDate.ToShortDateString()
                };
                return CreatedAtAction(nameof(Details), new{CourseId=createC.CourseId}, result);
            }
            return StatusCode(500, "Internal Server Error");
        }

        /// <summary>
        /// Lägger till en ny student till en befintlig kurs.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>Inget</returns>
        /// <response code="204"></response>
        /// <response code="404">Om kurs eller student inte finns i systemet</response>




        [HttpPatch("Addstudent/{CourseId}/{StudentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Addstudent(Guid CourseId, Guid StudentId)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == CourseId);
            var student = await _context.Students.SingleOrDefaultAsync(c => c.StudentId == StudentId);

            if(course is null) return NotFound("Kunde inte hitta kursen");
            if(student is null) return NotFound("Kunde inte hitta studenten");

        var StudentCourse = new StudentCourse
        {
            Course = course,
            Student = student
        };
            await _context.StudentCourse.AddAsync(StudentCourse);

            if(await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return StatusCode(500, "Internal Server Error");
        }
    }