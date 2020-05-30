using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LendCar.Repository;
using LendCar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;

namespace LendCar
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LendCarDBContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("LendCarCString")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IBrandModelRepository, BrandModelRepository>();
            services.AddTransient<IContactRepository,ContactRepository>();
            services.AddTransient<ICityRepostiory, CityRepostiory>();
            services.AddTransient<IColorRepository,ColorRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();

            services.AddSingleton<IEmail, Email>();
          
            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //services.AddSingleton<IEmailSender, EmailSender>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LendCarDBContext>().AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/Login";
            }
            );

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});

            services.AddControllers();
            services.AddRazorPages(c=>c.Conventions.AddPageRoute("/Home",""));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];


            app.UseHttpMethodOverride();


            app.UseHttpsRedirection();

            
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
