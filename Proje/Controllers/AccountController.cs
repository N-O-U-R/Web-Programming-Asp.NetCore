using Microsoft.AspNetCore.Mvc;

namespace Proje.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
