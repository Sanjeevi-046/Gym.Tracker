using Gym.Tracker.Core.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Core.Services.v1
{
    public interface IUserService
    {
        Task<object> SaveUserAsync(UserRequestModel saveUserServiceModel, long loginUserId, long loginUserRoleId);
        Task<UserVerificationResult> IsExistingUser(string email, string password);
        Task<List<UserRoleResult>> GetUserRoles();
    }
}
