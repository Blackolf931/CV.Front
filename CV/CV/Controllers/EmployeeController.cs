using System.Data;
using System.Net.Http.Headers;
using System.Text;
using CV.Constants;
using CV.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CV.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeList()
        {
            IEnumerable<Employee> employeeList = new List<Employee>();

            HttpClient client = new();
            HttpResponseMessage response = new ();
            response = await client.GetAsync(ApiUri.Employee);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result);
            }

            return View(employeeList);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var jsonObj = JsonConvert.SerializeObject(employee);
            HttpContent content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            HttpClient client = new();
            var response = await client.PostAsync(ApiUri.Employee, content);

            return RedirectToAction("EmployeeList");
        }
    }
}
