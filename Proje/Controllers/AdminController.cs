using Microsoft.AspNetCore.Mvc;

namespace Proje.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
