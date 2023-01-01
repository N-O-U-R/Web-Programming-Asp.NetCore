using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proje.Models;

namespace Proje.Controllers
{
    public class CallApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            var hhtc = new HttpClient();
            var response = await hhtc.GetAsync("https://localhost:7087/api/UserApi");
            string resString = await response.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<List<AppUser>>(resString);
            return View(users);
        }
    }
}
