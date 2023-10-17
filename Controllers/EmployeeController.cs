
using API.DTOs.Employees;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository repository;

        //public EmployeeController(ILogger<EmployeeController> logger)
        //{
        //    _logger = logger;
        //}
        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> List()
        {
            var result = await repository.Get();
            var listEmployee = new List<EmployeeDto>();
            if (result != null)
            {
                listEmployee = result.Data.ToList();
            }
            return View(listEmployee);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}