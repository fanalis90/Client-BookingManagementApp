using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, CreateEmployeeDto , Guid> , IEmployeeRepository
    {
        private readonly string _request;

        public EmployeeRepository(string request = "Employee/") : base(request)
        {
            _request = request;
          
        }

       

    }
}
