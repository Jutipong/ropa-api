using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UsersController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userAuthenticationService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetAll()
        {
            var users = _userAuthenticationService.GetAll();
            return Ok(users);
        }
    }
}
