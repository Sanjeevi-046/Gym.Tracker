using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Learn.Common.Extensions
{
    public static class ApiVersionExtension
    {
        /// <summary>
        /// Register api version
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;

                // You can combine readers (URL, Query, Header, MediaType)
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader()
                // new QueryStringApiVersionReader("api-version"),
                // new HeaderApiVersionReader("x-api-version")
                );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";  // e.g. v1, v1.1, v2
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
