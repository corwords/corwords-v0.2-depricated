﻿using Corwords.Web.Core.Configuration;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Models.Configuration;
using Corwords.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using WilderMinds.MetaWeblog;

namespace Corwords.Web
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
            // Add AppSettings
            services.Configure<AppSettings>(Configuration);
            services.ConfigureWritable<GeneralSettings>(Configuration.GetSection("GeneralSettings"), "corwords-settings.json");

            // Add database contexts
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CorwordsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
            });

            // Add MetaWeblog
            services.AddMetaWeblog<CorMetaWeblogService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> appSettings, ApplicationDbContext applicationDbContext, CorwordsDbContext corwordsDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            DbInitializer.Initialize(applicationDbContext, corwordsDbContext);

            app.UseETagger();

            var blogSettings = appSettings.Value.BlogSettings;
            app.UseMetaWeblog(blogSettings.MetaweblogEndpoint);

            app.UseMvc(routes =>
            {
                // First Run Route
                // Removed as we cannot currently remove routes without recycling the app.
                routes.MapRoute("firstrun", "{*firstrun}", new { controller = "Init", action = "Begin" }, new { firstrun = new FirstRunContraint() });

                // Dynamic Router
                routes.MapRoute("corwords", "{*corwords}", new { controller = "DynamicUrlRouter", action = "Route" }, 
                    new { corwords = new DynamicUrlConstraint(() => app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<CorwordsDbContext>()) });

                // Base Route
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}