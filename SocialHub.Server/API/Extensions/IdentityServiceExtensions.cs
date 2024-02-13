using Domain;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

                options.User.RequireUniqueEmail = true;

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsActivityHost", 
                    policy => policy.Requirements.Add(new IsHostRequirement()));
            });
            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
            services.AddScoped<TokenService>();

            return services;            
        }

    }
}
