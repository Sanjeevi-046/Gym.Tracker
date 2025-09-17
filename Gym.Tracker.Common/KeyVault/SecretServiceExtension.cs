using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.Tracker.Common.KeyVault
{
    public static class SecretServiceExtensions
    {
        public static IServiceCollection AddSecretServices(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services = services.ConfigureKeyVaultSettings(configuration);

            
            return services;
        }

        /// <summary>
        /// Register types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureKeyVaultSettings(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration["ConnectionStrings:DefaultConnection"] = configuration[Resources.Resources.ConnectionString];
           
            configuration["EmailConfig:ConnectionString"] = configuration["EmailConfig:ConnectionString"];
            configuration["EmailConfig:SenderEmail"] = configuration["EmailConfig:SenderEmail"];

            configuration["BasicAuthenticationCredentials:UserName"] = configuration["BasicAuthenticationCredentials:UserName"];
            configuration["BasicAuthenticationCredentials:Password"] = configuration["BasicAuthenticationCredentials:Password"];

            configuration["token"] = configuration["token"];

            return services;
        }
    }
}
