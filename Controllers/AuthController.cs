using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            
                return RedirectToAction("Index", "Employee");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginDto loginDto)
        {
            var result = await _accountRepository.Login(loginDto);
            if(result.Status == "OK")
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                TempData["login"] = "Login Success";
                return RedirectToAction("List", "Employee");
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginDto loginDto)
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
