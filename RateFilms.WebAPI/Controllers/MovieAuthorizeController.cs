using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MovieAuthorizeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("I autorize");
        }
    }
}
