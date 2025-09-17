using Gym.Tracker.Core.ServiceModel;

namespace Gym.Tracker.Core.Services.v1
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateUser(LoginRequest loginRequest);
    }
}
