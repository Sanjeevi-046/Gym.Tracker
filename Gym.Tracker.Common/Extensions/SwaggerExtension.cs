using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Auth.Learn.Common.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Register Swagger with API versioning
        /// </summary>
        public static void RegisterSwaggerAuthorization(this IServiceCollection services, string xmlDocumentationFileName)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                // Add JWT Bearer support
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Include XML comments if available
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFileName);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            // Hook into versioned API explorer so swagger generates docs per API version
            services.ConfigureOptions<SwaggerVersioningOptions>();
        }
    }

    /// <summary>
    /// Configures Swagger docs for each API version
    /// </summary>
    public class SwaggerVersioningOptions : Microsoft.Extensions.Options.IConfigureOptions<Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerVersioningOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new OpenApiInfo
                {
                    Title = "Gym Tracker",
                    Version = desc.ApiVersion.ToString(),
                    Description = "API documentation with versioning support"
                });
            }
        }
    }
}
