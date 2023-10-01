using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ClientIdPolicy")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return new JsonResult(claims);
        }
    }
}
