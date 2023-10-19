using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<AccountDto, CreateAccountDto, Guid>
    {
        public Task<ResponseOkHandler<TokenDto>> Login(LoginDto login);
    }
}
