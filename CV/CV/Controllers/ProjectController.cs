using System.Text;
using CV.Constants;
using CV.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CV.Controllers
{
    public class ProjectController : Controller
    {
        private readonly HttpClient _client;

        public ProjectController()
        {
            _client = new();
        }

        [HttpGet]
        public async Task<IActionResult> ProjectList()
        {
            IEnumerable<Project> employeeList = new List<Project>();
            var response = await _client.GetAsync(ApiUri.Project);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<IEnumerable<Project>>(result);
            }

            return View(employeeList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectAsync(Project project)
        {
            var jsonObj = JsonConvert.SerializeObject(project);
            HttpContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUri.Project, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProjectList");
            }

            return RedirectToAction("ProjectList");
        }

        public async Task<IActionResult> EditProjectAsync(int id)
        {
            Project project;
            var response = await _client.GetAsync(ApiUri.Project + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                project = JsonConvert.DeserializeObject<Project>(data);

                return View(project);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProjectAsync(Project project)
        {
            var jsonObj = JsonConvert.SerializeObject(project);
            HttpContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ApiUri.Project}/{project.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProjectList");
            }

            return View("EditProject", project);
        }

        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            var response = await _client.DeleteAsync($"{ApiUri.Project}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProjectList");
            }

            return RedirectToAction("ProjectList");
        }
    }
}
