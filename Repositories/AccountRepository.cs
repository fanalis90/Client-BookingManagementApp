using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Contracts;
using Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<AccountDto, CreateAccountDto, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "Account/") : base(request)
        {
        }

        public async Task<ResponseOkHandler<TokenDto>> Login(LoginDto login)
        {
            ResponseOkHandler<TokenDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login/", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<TokenDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
