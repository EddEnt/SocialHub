using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using SocialHub.Server.API.Services;
using System.Text;

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

            //Hardcoded key for now, to be changed later
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Users:SecurityKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false, //Issuer is the API server
                        ValidateAudience = false //Audience is the client app
                    };
                });


            services.AddScoped<TokenService>();

            return services;            
        }

    }
}
