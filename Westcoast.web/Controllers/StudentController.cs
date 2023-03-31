using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Westcoast.web.ViewModels.Students;

namespace Westcoast.web.Controllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public StudentController(IConfiguration config, IHttpClientFactory httpClient)
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
            
            var response = await client.GetAsync($"{_baseUrl}/students/listall");

            if (!response.IsSuccessStatusCode) return Content("Oops gick fel!");

            var json = await response.Content.ReadAsStringAsync();

            var students = JsonSerializer.Deserialize<IList<StudentListViewModel>>(json, _options);

            return View("Index", students);
        }

        [HttpGet("Details/{StudentId}")]
        public async Task<IActionResult> Details(Guid StudentId)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/students/Details/{StudentId}");

            // if (!response.IsSuccessStatusCode) return Content("Oops gick fel");

            var json = await response.Content.ReadAsStringAsync();
            var students = JsonSerializer.Deserialize<StudentDetailsViewModel>(json, _options);

            return View("Details", students);
        }

    }
}