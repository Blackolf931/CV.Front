using CV.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee(0, "Name1", "Surname1", "Patronymic1"));
            employees.Add(new Employee(0, "Name2", "Surname2", "Patronymic2"));
            employees.Add(new Employee(0, "Name3", "Surname3", "Patronymic3")); 
            employees.Add(new Employee(0, "Name4", "Surname4", "Patronymic4"));
            employees.Add(new Employee(0, "Name5", "Surname5", "Patronymic5"));

            return View(employees);
        }
    }
}
