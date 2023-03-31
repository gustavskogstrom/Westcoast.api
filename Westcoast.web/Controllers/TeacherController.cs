using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Westcoast.web.ViewModels.Teachers;

namespace Westcoast.web.Controllers
{
    [Route("Teacher")]
    public class TeacherController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public TeacherController(IConfiguration config, IHttpClientFactory httpClient)
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
            
            var response = await client.GetAsync($"{_baseUrl}/teachers/listall");

            if (!response.IsSuccessStatusCode) return Content("De fungerar inte...");

            var json = await response.Content.ReadAsStringAsync();

            var teachers = JsonSerializer.Deserialize<IList<TeacherListViewModel>>(json, _options);

            return View("Index", teachers);
        }

        [HttpGet("Details/{TeacherId}")]
        public async Task<IActionResult> Details(Guid TeacherId)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/teachers/Details/{TeacherId}");

            if (!response.IsSuccessStatusCode) return Content("Oops gick fel");

            var json = await response.Content.ReadAsStringAsync();
            var teachers = JsonSerializer.Deserialize<TeacherDetailsViewModel>(json, _options);

            return View("Details", teachers);
        }
    }
}