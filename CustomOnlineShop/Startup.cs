using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CustomOnlineShop.Services;
using ApplicationInfrastructure.Data;
using ApplicationInfrastructure.Identity;
using ApplicationCore.Interfaces.Data;
using ApplicationInfrastructure.Data.Repositories;
using CustomOnlineShop.Services.Interfaces;

namespace CustomOnlineShop
{
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        // Real database
            //services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:OnlineShop"]));
            //services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:OnlineShopIdentity"]));
            //services.AddScoped<ICartService, CartService>();

        // In memory database
            services.AddDbContext<OnlineShopDbContext>(options => options.UseInMemoryDatabase("SHopDB"));
            services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseInMemoryDatabase("IdentityDB"));
            services.AddScoped<ICartService, CachedCartService>();

            services.AddMvc();
            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddScoped<IProductReviewRepository, EFProductReviewRepository>();
            services.AddScoped<IProductCategoryRepository, EFProductCategoryRepository>();
            services.AddScoped<IShippingInfoRepository, EFShippingInfoRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<ICookieService, CookieService>();
            services.AddScoped<IUserValidator<AppUser>, CustomUserValidator>();
            services.AddScoped<IPasswordValidator<AppUser>, CustomPasswordValidator>();
            services.AddScoped<ICartRepository, EFCartRepository>();
            services.AddSession();

            // Add Identity
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                // Set user name policy
                opts.User.RequireUniqueEmail = true;

                // Set password policy
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
