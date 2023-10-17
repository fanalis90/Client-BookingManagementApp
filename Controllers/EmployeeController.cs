
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
        private readonly IEmployeeRepository _emplyeeRepository;

        //public EmployeeController(ILogger<EmployeeController> logger)
        //{
        //    _logger = logger;
        //}
        public EmployeeController(IEmployeeRepository repository)
        {
            _emplyeeRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> List()
        {
            var result = await _emplyeeRepository.Get();
            var listEmployee = new List<EmployeeDto>();
            if (result.Data != null)
            {
                listEmployee = result.Data.ToList();
            }
            return View(listEmployee);
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCLient(CreateEmployeeDto createEmployeeDto)
        {
            var result = await _emplyeeRepository.Post(createEmployeeDto);
            if(result != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClient(Guid guid)
        {
            var result = await _emplyeeRepository.Get(guid);
            var Employee = new EmployeeDto();
            if (result.Data != null)
            {
                Employee = result.Data;
            }
            return View(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCLient(EmployeeDto EmployeeDto)
        {
            var result = await _emplyeeRepository.Put(EmployeeDto.Guid,EmployeeDto);
            if(result != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteCLient(CreateEmployeeDto createEmployeeDto)
        {
            var result = await _emplyeeRepository.Post(createEmployeeDto);
            if(result != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}