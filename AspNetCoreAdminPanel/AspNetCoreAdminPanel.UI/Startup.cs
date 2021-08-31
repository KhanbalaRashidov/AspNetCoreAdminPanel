using AspNetCoreAdminPanel.Business.Abstract;
using AspNetCoreAdminPanel.Business.Concrete.Managers;
using AspNetCoreAdminPanel.DataAccess.Abstract;
using AspNetCoreAdminPanel.DataAccess.Concrete.EntityFrameworkCore;
using AspNetCoreAdminPanel.WebUI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAdminPanel.UI.Services;

namespace AspNetCoreAdminPanel.UI
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

            services.AddDbContext<AppIdentityDbContext>(option => option.UseSqlServer(Configuration["AdminDbConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/Security/Login";
                options.LogoutPath = "/Security/LogOut";
                options.AccessDeniedPath = "/Security/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie = new CookieBuilder { 
                HttpOnly = true,
                Name="AspNetCoreAdminPanel.Cookie",
                Path="/",
                SameSite=SameSiteMode.Lax,
                SecurePolicy=CookieSecurePolicy.SameAsRequest
                };
            });
            
            
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductdal, EfProductDal>();

            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IProductImageDal, EfProductImageDal>();

            services.AddScoped<IEmailConfiguration, EmailConfiguration>();
            services.AddScoped<IMailService, MailManager>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=GetProducts}/{id?}");
            });
        }
    }
}
