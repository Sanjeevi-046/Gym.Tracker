using Gym.Tracker.Core.ServiceModel;
using Gym.Tracker.Core.Services.v1;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Dependency Injection for Auth Service
        private readonly IAuthService _authService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public LoginController(IAuthService authService)
        {
            this._authService = authService;
        }

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <remarks>This method delegates the authentication process to the underlying authentication
        /// service. Ensure that the <paramref name="loginRequest"/> contains valid credentials before calling this
        /// method.</remarks>
        /// <param name="loginRequest">The login request containing the user's credentials, such as username and password.</param>
        /// <returns>An <see cref="IActionResult"/> containing the authentication result. Typically, this includes a success
        /// response with user details or a token if authentication is successful, or an error response if
        /// authentication fails.</returns>
        [HttpPost]
        public async Task<IActionResult> LoginUser (LoginRequest loginRequest)
        {
            return Ok(await _authService.AuthenticateUser(loginRequest));
        }
    }
}
