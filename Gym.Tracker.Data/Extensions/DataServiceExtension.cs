using Gym.Tracker.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Data.Extensions
{
    public static class DataServiceExtension
    {
        /// <summary>
        /// Adding the DbConnection Service 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddDataConnector(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            ArgumentNullException.ThrowIfNull(configuration);

            var sqlConnection = configuration.GetConnectionString("DefaultConnection");

            //Ensures SQL queries EF generates are compatible with a specific SQL Server version (160 - SQL server 2022 ).
            //ServiceLifetime.Scoped - One instance per HTTP request , each request has its own isolated unit of work , prevents Threading Issue.
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(sqlConnection, o => o.UseCompatibilityLevel(160)), ServiceLifetime.Scoped 
            );
            return services;
        }
    }
}
