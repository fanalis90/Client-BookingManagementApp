using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    [Authorize]
    public class EmployeeRepository : GeneralRepository<EmployeeDto, CreateEmployeeDto , Guid> , IEmployeeRepository
    {


        public EmployeeRepository(string request = "Employee/") : base(request)
        {
 
          
        }
        public async Task<ResponseOkHandler<EmployeeDetailsDto>> GetDetail(Guid id)
        {
            ResponseOkHandler<EmployeeDetailsDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync(request + "details/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<EmployeeDetailsDto>>(apiResponse);
            }
            return entityVM;
        }




    }
}
