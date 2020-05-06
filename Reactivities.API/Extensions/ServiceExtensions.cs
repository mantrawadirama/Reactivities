using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactivities.Persistence;

namespace Reactivities.API.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureSqliteDbContext (this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<DataContext> (opt => {
                opt.UseSqlite (config.GetConnectionString ("DefaultConnection"));
            });
        }
    }

}