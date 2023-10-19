
using API.DTOs.Employees;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Client.Controllers
{
    [Authorize]
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
        [Authorize(Policy = "Manager")]
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
        public async Task<IActionResult> Detail(Guid guid)
        {
            var result = await _emplyeeRepository.GetDetail(guid);
            var listEmployee = new EmployeeDetailsDto();
            if (result.Data != null)
            {
                listEmployee = result.Data;
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
                TempData["success"] = "Created!Your data has been created.";
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
                TempData["success"] = "Updated!Your data has been updated."; 
                return RedirectToAction("List");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteClient(Guid guid)
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
        public async Task<IActionResult> DeleteCLient(EmployeeDto employeeDto)
        {
            var result = await _emplyeeRepository.Delete(employeeDto.Guid);
            if(result != null)
            {
                TempData["success"] = "Deleted!Your data has been deleted.";
                return RedirectToAction("List");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> ListJson()
        {
            var result = await _emplyeeRepository.Get();
            var listEmployee = new ResponseOkHandler<IEnumerable<EmployeeDto>>();
            if (result.Data != null)
            {
                listEmployee = result;
            }
            return Json(listEmployee);
        }
    }
}