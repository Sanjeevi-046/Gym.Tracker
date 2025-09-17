using Gym.Tracker.Core.ServiceModel;
using Gym.Tracker.Core.Services.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser (LoginRequest loginRequest)
        {
            return Ok(await _authService.AuthenticateUser(loginRequest));
        }
    }
}
