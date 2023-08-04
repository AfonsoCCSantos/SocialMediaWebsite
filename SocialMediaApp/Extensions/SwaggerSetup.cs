using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace SocialMedia.API.Extensions
{
    public static class SwaggerSetup
    {
        public static IServiceCollection SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization Header using the Bearer Scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return services;
        }
    }
}
