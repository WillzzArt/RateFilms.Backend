using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MovieController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("I autorize");
        }
    }
}
