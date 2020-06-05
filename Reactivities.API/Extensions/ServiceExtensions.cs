using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Reactivities.Application.Activities;
using Reactivities.Application.Interfaces;
using Reactivities.Domain;
using Reactivities.Infrastructure;
using Reactivities.Infrastructure.Photos;
using Reactivities.Infrastructure.Security;
using Reactivities.Persistence;

namespace Reactivities.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").AllowCredentials();
                });
            });
        }
        public static void ConfigureSqliteDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
        }

        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(List.Handler).Assembly);
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                // validates all requests are authorized
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Create>();
            });
        }
        public static void ConfigureSignInService(this IServiceCollection services)
        {
            // this method provides API ability to create and manage service via UserManager service
            // AppUser is class defined in the application doamin
            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            // this helps sign-in with username and password 
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
        }

        public static void ConfigureAuthorizationPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsActivityHost", policy =>
                {
                    policy.Requirements.Add(new IsHostRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
        public static void ConfigureJwtGenerator(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        public static void ConfigureUserAccessor(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessor, UserAccessor>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(List.Handler));
        }
        public static void ConfigureCloudinary(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
        }

        public static void ConfigurePhotoAccessor(this IServiceCollection services)
        {
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
        }
    }

}