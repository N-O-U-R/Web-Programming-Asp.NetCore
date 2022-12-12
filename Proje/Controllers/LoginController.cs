using Microsoft.AspNetCore.Mvc;

namespace Proje.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
