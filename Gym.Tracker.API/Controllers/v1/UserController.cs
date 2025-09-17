using Asp.Versioning;
using Gym.Tracker.Core.ServiceModel;
using Gym.Tracker.Core.Services.v1;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Tracker.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        /// <summary>
        /// Method to Save user details.
        /// </summary>
        /// <param name="userServiceModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveUser([FromBody] UserRequestModel userRequestModel)
        {
            var result = await _userService.SaveUserAsync(userRequestModel, 1, 1);
            return Ok(result);
        }

        /// <summary>
        /// Gets all user roles.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves all roles from the system by calling the user service.
        /// The result contains a list of roles with their Id, RoleCode, and RoleName.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="UserRoleResult"/> objects.
        /// </returns>
        /// <response code="200">Returns the list of user roles.</response>

        [HttpGet]
        public async Task<IActionResult>GetUserRoles()
        {
            var result = await _userService.GetUserRoles();
            return Ok(result);
        }
    }
}
