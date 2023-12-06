using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4PawsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<string> Get()
        {
            return "Hello, asshole!";
        }
    }
}
