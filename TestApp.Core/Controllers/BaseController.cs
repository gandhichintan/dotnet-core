using TestApp.Core.Services;
using TestApp.DomainModel.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestApp.Core.Controllers
{
    public class BaseController : Controller
    {
        public BankAccount BankAccount { get { return HttpContext.Session.GetObjectFromJson<BankAccount>("bankAccount"); } }

        public BaseController()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsAuthenticated = BankAccount != null;
            base.OnActionExecuting(context);
        }
    }
}
