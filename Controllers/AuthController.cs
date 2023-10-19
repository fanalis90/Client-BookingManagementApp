using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AuthController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult LoginClient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginClient(LoginDto loginDto)
        {
            var result = await _accountRepository.Login(loginDto);
            if(result.Status == "OK")
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("List", "Employee");
            }
            return View();
        }
    }
}
