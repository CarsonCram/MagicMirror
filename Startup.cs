using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MagicMirror.Data;
using Microsoft.EntityFrameworkCore;
using MagicMirror.Models;
using Microsoft.AspNetCore.Identity;
using MagicMirror.Infrastructure;

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

            services.AddTransient<IPasswordValidator<AppUser>,
                CustomPasswordValidator>();
            services.AddTransient<IUserValidator<AppUser>,
                CustomUserValidator>();

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["Data:MirrorIdentity:ConnectionString"]));
            services.AddDbContext<GoalContext>(options =>
                options.UseSqlServer(Configuration["Data:GoalContext:ConnectionString"]));
            
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                //opts.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm";
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices,
            //    Configuration).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
                        