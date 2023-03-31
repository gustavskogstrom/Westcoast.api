using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Westcoast.web.ViewModels;
using WestcoastMVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WestcoastMVC.Controllers
{
    [Route("courses")]
    public class CoursesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public CoursesController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            
            var response = await client.GetAsync($"{_baseUrl}/courses");

            if (!response.IsSuccessStatusCode) return Content("Oops gick fel!");

            var json = await response.Content.ReadAsStringAsync();

            var courses = JsonSerializer.Deserialize<IList<CourseListViewModel>>(json, _options);

            return View("Index", courses);
        }

        [HttpGet("Details/{CourseId}")]
        public async Task<IActionResult> Details(Guid CourseId)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses/Details/{CourseId}");

            if (!response.IsSuccessStatusCode) return Content("Oops gick fel");

            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<CourseDetailsViewModel>(json, _options);

            return View("Details", course);
        }
    }
}
