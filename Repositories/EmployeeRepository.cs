using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, CreateEmployeeDto , Guid> , IEmployeeRepository
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {
        }

    }
}
