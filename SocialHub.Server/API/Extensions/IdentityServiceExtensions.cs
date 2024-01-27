using Domain;
using Persistence;
using SocialHub.Server.API.Services;

namespace SocialHub.Server.API.Extensions
{
    public static class IdentityServiceExtensions
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 10;

            }).AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication();

            services.AddScoped<TokenService>();

            return services;            
        }

    }
}
