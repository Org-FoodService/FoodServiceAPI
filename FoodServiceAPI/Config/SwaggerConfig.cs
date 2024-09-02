using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace FoodServiceAPI.Config
{
    /// <summary>
    /// Static class responsible for configuring Swagger for the FoodServiceAPI.
    /// </summary>
    public static class SwaggerConfig
    {
        private static readonly string[] value = [];

        /// <summary>
        /// Configures Swagger with the specified settings, including security definitions and annotations.
        /// </summary>
        /// <param name="services">The service collection to which Swagger services will be added.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Define the Swagger document with basic API information
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodServiceAPI", Version = "v1" });

                // Enable Swagger annotations for better documentation and control
                c.EnableAnnotations();

                // Enable example filters for request and response examples
                c.ExampleFilters();

                // Add security definition for JWT Bearer tokens
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                // Require JWT Bearer token for accessing API endpoints
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
                        value 
                    }
                });
            });

            // Register request examples for Swagger documentation
            RegisterRequestExamples(services);
        }

        /// <summary>
        /// Registers Swagger examples from classes within the specified namespace.
        /// </summary>
        /// <param name="services">The service collection to which the examples will be added.</param>
        private static void RegisterRequestExamples(IServiceCollection services)
        {
            // Get all classes within the namespace "FoodServiceAPI.Controllers.SwaggerRequestExample"
            var exampleTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Namespace == "FoodServiceAPI.Controllers.SwaggerRequestExample")
                .ToList();

            // Register each example class found in the specified namespace
            foreach (var exampleType in exampleTypes)
            {
                services.AddSwaggerExamplesFromAssemblyOf(exampleType);
            }
        }
    }
}
