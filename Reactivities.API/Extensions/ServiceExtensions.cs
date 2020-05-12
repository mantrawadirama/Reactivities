using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactivities.Application.Activities;
using Reactivities.Persistence;

namespace Reactivities.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors (this IServiceCollection services)
        {
            services.AddCors (opt =>
            {
                opt.AddPolicy ("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader ().AllowAnyMethod ().WithOrigins ("http://localhost:3000");
                });
            });
        }
        public static void ConfigureSqliteDbContext (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext> (opt =>
            {
                opt.UseSqlite (config.GetConnectionString ("DefaultConnection"));
            });
        }

        public static void ConfigureMediator (this IServiceCollection services)
        {
            services.AddMediatR (typeof (List.Handler).Assembly);
        }

    }

}