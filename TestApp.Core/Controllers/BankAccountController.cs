using Microsoft.AspNetCore.Mvc;

namespace TestApp.Core.Controllers
{
    public class BankAccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
