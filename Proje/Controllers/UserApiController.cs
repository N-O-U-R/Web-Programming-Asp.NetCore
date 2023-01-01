using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proje.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserApiController : ControllerBase
	{

        private readonly ShowContext _context;

        public UserApiController(ShowContext context)
        {
            _context = context;
        }


        // GET: api/<UserApiController>
        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> Get()
        {
            var u = await _context.Users.ToListAsync();
            if (u is null)
            {
                return NoContent();
            }
            return u;

        }
    }
}
