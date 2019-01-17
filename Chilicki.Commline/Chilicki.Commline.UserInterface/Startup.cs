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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using Chilicki.Commline.Domain.Search.Factories.StopConnections;
using Chilicki.Commline.Domain.Services.Lines;
using Chilicki.Commline.Domain.Search.Services.Descriptions;

namespace Chilicki.Commline.UserInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookies(services);
            ConfigureDatabase(services);
            ConfigureDependencyInjection(services);
            ConfigureMappings(services);
            ConfigureMvc(services);
        }

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

            app.UseRequestLocalization();
            var cultureInfo = new CultureInfo("en-GB");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            AppDomain.CurrentDomain.SetData("DataDirectory", env.ContentRootPath);

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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connection = @"Server=localhost;Database=Commline;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<CommlineDBContext>
                (options => options.UseSqlServer(connection));
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<StopRepository>();
            services.AddTransient<LineRepository>();
            services.AddTransient<DepartureRepository>();
            services.AddTransient<RouteStopRepository>();
            services.AddTransient<MixedRepository>();

            services.AddTransient<StopLineTypesMatchChecker>();
            services.AddTransient<NextRouteStopResolver>();
            services.AddTransient<LineDirectionService>();

            services.AddTransient<IConnectionSearchEngine, DijkstraConnectionSearchEngine>();
            services.AddTransient<IGraphFactory<StopGraph>, StopGraphFactory>();
            services.AddTransient<StopConnectionFactory>();            
            services.AddTransient<DijkstraEmptyFastestConnectionsFactory>();
            services.AddTransient<DijkstraNextVertexResolver>();
            services.AddTransient<DijkstraFastestConnectionReplacer>();
            services.AddTransient<DijkstraStopConnectionsService>();
            services.AddTransient<DijkstraStopGraphService>();

            services.AddTransient<FastestPathResolver>();
            services.AddTransient<FastestPathTransferService>();
            services.AddTransient<StopConnectionCloner>();
            services.AddTransient<FastestPathDescriptionWriter>();
            services.AddTransient<FastestPathTimeCalculator>();

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
            services.AddTransient<DepartureRunCorrector>();

            services.AddTransient<SearchInputManualMapper>();

            services.AddTransient<SettingsManager>();
            services.AddTransient<SettingsSerializer>();
            services.AddTransient<SettingsDeserializer>();

            services.AddTransient<HomeController>();
            services.AddTransient<EditorController>();
            services.AddTransient<SearchController>();
            services.AddTransient<SettingsController>();
        }

        private void ConfigureMappings(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }
    }
}
