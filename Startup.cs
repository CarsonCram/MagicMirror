using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MagicMirror.Data;
using MagicMirror.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicMirror
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
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<GoalContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:GoalContext:ConnectionString"]));
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Goal", action = "Index" }
                );
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            SeedData.Initialize(app);
        }
    }
}
