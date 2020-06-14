using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reactivities.API.Extensions;
using Reactivities.API.Middleware;
using Reactivities.API.SignalR;

namespace Reactivities.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureDevelopmentServices(Configuration);
            services.ConfigureProductionServices(Configuration);
            services.ConfigureFluentValidation();
            services.ConfigureCors();
            services.ConfigureMediator();
            services.ConfigureAutoMapper();
            services.AddSignalR();
            services.ConfigureSignInService();
            services.ConfigureAuthorizationPolicy();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureJwtGenerator();
            services.ConfigureUserAccessor();
            services.ConfigurePhotoAccessor();
            services.ConfigureProfileReader();
            services.ConfigureCloudinary(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                //  app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
                //This is react in-case if react app is hosted on same port as API
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}