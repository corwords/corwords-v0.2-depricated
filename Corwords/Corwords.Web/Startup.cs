﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Services;
using WilderMinds.MetaWeblog;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Routing;
using Corwords.Web.Extensions;

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

            // Add database contexts
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CorwordsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add MetaWeblog
            services.AddMetaWeblog<CorMetaWeblogService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext applicationDbContext, CorwordsDbContext corwordsDbContext)
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

            app.UseMetaWeblog("/metaweblog");

            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // First Run Route
            routeBuilder.MapRoute("firstrun", "{*firstrun}", defaults: new { controller = "Init", action = "Index" }, constraints: new { firstrun = new FirstRunContraint() });

            // Base Route
            routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
