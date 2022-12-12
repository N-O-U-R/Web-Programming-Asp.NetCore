using Microsoft.AspNetCore.Mvc;

namespace Proje.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
