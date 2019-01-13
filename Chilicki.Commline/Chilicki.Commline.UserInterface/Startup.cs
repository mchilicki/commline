using Chilicki.Commline.Application.Correctors;
using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.Application.Search.Managers;
using Chilicki.Commline.Application.Search.ManualMappers;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Factories;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.GraphFactories;
using Chilicki.Commline.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Commline.Domain.Search.Services.Path;
using Chilicki.Commline.Domain.Services.Matching;
using Chilicki.Commline.Domain.Services.Routes;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.IO;
using Chilicki.Commline.Infrastructure.Repositories;
using Chilicki.Commline.UserInterface.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chilicki.Commline.UserInterface
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
            ConfigureCookies(services);

            ConfigureDependencyInjection(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureCookies(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddDbContext<CommlineDBContext>();

            services.AddTransient<StopRepository>();
            services.AddTransient<LineRepository>();
            services.AddTransient<DepartureRepository>();
            services.AddTransient<RouteStopRepository>();
            services.AddTransient<MixedRepository>();

            services.AddTransient<StopLineTypesMatchChecker>();
            services.AddTransient<NextRouteStopResolver>();

            services.AddTransient<IConnectionSearchEngine, DijkstraConnectionSearchEngine>();
            services.AddTransient<IGraphFactory<StopGraph>, StopGraphFactory>();
            services.AddTransient<DijkstraNextVertexResolver>();
            services.AddTransient<DijkstraEmptyFastestConnectionsFactory>();
            services.AddTransient<DijkstraFastestConnectionReplacer>();

            services.AddTransient<FastestPathResolver>();
            services.AddTransient<FastestPathTransferService>();

            services.AddTransient<StopFactory>();
            services.AddTransient<LineFactory>();

            services.AddTransient<SearchManager>();
            services.AddTransient<StopManager>();
            services.AddTransient<LineManager>();
            services.AddTransient<DepartureManager>();
            services.AddTransient<EditorManager>();

            services.AddTransient<SearchValidator>();
            services.AddTransient<LineValidator>();
            services.AddTransient<DeparturesValidator>();
            services.AddTransient<StopValidator>();

            services.AddTransient<LineCorrector>();

            services.AddTransient<SearchInputManualMapper>();

            services.AddTransient<SettingsManager>();
            services.AddTransient<SettingsSerializer>();
            services.AddTransient<SettingsDeserializer>();

            services.AddTransient<HomeController>();
            services.AddTransient<EditorController>();
            services.AddTransient<SearchController>();
            services.AddTransient<SettingsController>();
        }
    }
}
