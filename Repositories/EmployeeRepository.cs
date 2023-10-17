using API.DTOs.Employees;
using API.Models;
using Client.Contracts;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<API.DTOs.Employees.EmployeeDto, Guid> , IEmployeeRepository
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {
        }
    }
}
