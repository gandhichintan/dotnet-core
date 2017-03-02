using System.Threading.Tasks;
using TestApp.Core.Services;
using TestApp.Core.Services.Banking;
using TestApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Core.Controllers
{
    public class AuthenticationController : BaseController
    {
        #region "Private Member(s)"
        private IBankingService _bankingService;
        #endregion

        public AuthenticationController(IBankingService bankingService)
        {
            _bankingService = bankingService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var loginVM = new LoginVM
            {
                Username = "testaccount@bank.com",
                Password = "Test@ccount1"
            };

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (ModelState.IsValid)
            {
                var bankAccount = await _bankingService.GetBankAccountInfo(loginVM.Username);

                if (bankAccount != null)
                {
                    HttpContext.Session.SetObjectAsJson("bankAccount", bankAccount);
                    return RedirectToRoute("dashboard");
                }
                ModelState.AddModelError("authenticationError", "Invalid username/password.");
            }
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
