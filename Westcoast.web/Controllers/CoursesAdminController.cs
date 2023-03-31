using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Westcoast.web.Models;
using Westcoast.web.ViewModels;
using Westcoast.web.ViewModels.Courses;
using static System.Net.Mime.MediaTypeNames;

namespace Westcoast.web.Controllers
{
    [Route("CoursesAdmin")]
    public class CoursesAdminController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public CoursesAdminController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var course = new CoursePostViewModel();

            return View("Create", course);

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CoursePostViewModel course)
        {
            if (!ModelState.IsValid) return View("Create", course);

            var Create = new 
            {
                CourseId = course.CourseId,
                CourseNumber = course.CourseNumber,
                CourseTitle = course.CourseTitle,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                CourseLenght = course.CourseLenght,
                Description = course.Description
            };
            
            using var client = _httpClient.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(Create), Encoding.UTF8, Application.Json);

            var response = await client.PostAsync($"{_baseUrl}/courses/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Content("De funkar inte? :I");
        }
        
    }
}