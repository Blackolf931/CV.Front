﻿using System.Text;
using CV.Constants;
using CV.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CV.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeList()
        {
            IEnumerable<Employee> employeeList = new List<Employee>();
            var response = await client.GetAsync(ApiUri.Employee);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result);
            }

            return View(employeeList);
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
        {
            var data = JsonConvert.SerializeObject(employee);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ApiUri.Employee, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }

            return View("CreateEmployee");
        }

        public async Task<IActionResult> EditEmployeeAsync(int id)
        {
            Employee employee;
            var response = await client.GetAsync(ApiUri.Employee + $"/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(data);

                return View(employee);
            }

            return View();
        }

        [HttpPost]
        public IActionResult EditEmployeeAsync(Employee employee)
        {
            string data = JsonConvert.SerializeObject(employee);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = client.PutAsync(ApiUri.Employee + $"/{employee.Id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }

            return View("EditEmployee", employee);
        }

        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var response = await client.DeleteAsync(ApiUri.Employee + $"/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EmployeeList");
            }

            return View("EmployeeList");
        }
    }
}
