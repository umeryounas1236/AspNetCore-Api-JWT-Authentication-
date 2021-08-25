using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNet_Core_API__JWT_Authentication_.JwtService
{
    public static class JWTServiceRegisterExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opts => {
                opts.DefaultScheme =
                 JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
            });

            services.Configure<JWTSettings>(configuration.GetSection("Jwt"));

            services.AddSingleton<IJwtHandler, JWTHandler>();

            return services;
        }
    }
}
