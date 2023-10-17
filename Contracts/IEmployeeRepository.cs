using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<EmployeeDto, CreateEmployeeDto, Guid>
    {

        public Task<ResponseOkHandler<EmployeeDetailsDto>> GetDetail(Guid id);
    }
}
