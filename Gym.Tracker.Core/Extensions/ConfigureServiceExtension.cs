using Gym.Tracker.Core.Services.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Tracker.Core.Extensions
{
    /// <summary>
    /// Business Service Collection Extension
    /// </summary>
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection AddServiceConnector(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
