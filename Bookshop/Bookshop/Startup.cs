using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookshop.Data;
using Bookshop.Data.Interfaces;
using Bookshop.Data.Repositories;
using Bookshop.Models;
using Bookshop.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Bookshop
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IApplicationBuilder AddErrorHandling(IApplicationBuilder options)
        {
            options.Run(
                async context =>
                {
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    var ip = context.Connection.LocalIpAddress.ToString();
                    if (ex != null && ip.Contains("127.0.0.1"))
                    {
                        var err = $"<h1>Your IP: {ip}</h1><br/><h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                        await context.Response.WriteAsync(err);
                    }
                    else
                    {
                        var err = $"<h1>Your IP: {ip}</h1><br/><h1>Error!</h1>";
                        await context.Response.WriteAsync(err);
                    }
                });

            return options;
        }
    }
}
