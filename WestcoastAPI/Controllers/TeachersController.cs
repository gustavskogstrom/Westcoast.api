using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastAPI.Data;
using WestcoastAPI.Models;
using WestcoastAPI.ViewModels;
using WestcoastAPI.ViewModels.Teachers;

namespace WestcoastAPI.Controllers;

    [ApiController]
    [Route("api/v1/teachers")]
    public class TeachersController : ControllerBase
    {

    private readonly WestcoastContext _context;
    public TeachersController(WestcoastContext context)
    {
            _context = context;
    }

    [HttpGet("ListAll")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Teachers
            .Select(t => new
            {
                TeacherId = t.TeacherId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email
                
            })
            .ToListAsync();
            return Ok(result);
        }


        [HttpGet("Details/{TeacherId}")]
        public async Task<IActionResult> Details(Guid TeacherId)
        {
            var result = await _context.Teachers
            .Select(t => new
            {
                TeacherId = t.TeacherId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                SecurityNumber = t.SecurityNumber,
                Phone = t.Phone,
                StreetAdress = t.StreetAdress,
                Courses = t.Courses.Select(c => new
                    {
                        CourseId = c.CourseId,
                        CourseTitle = c.CourseTitle
                    }).ToList(),
            })
            .SingleOrDefaultAsync(t => t.TeacherId == TeacherId);
                return Ok(result);
        }

    }
    