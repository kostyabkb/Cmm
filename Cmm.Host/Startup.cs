using System;
using Cmm.Contracts;
using Cmm.Host.Migrations;
using Cmm.Host.Model;
using Cmm.Host.Repositories;
using Cmm.Host.Services;
using Cmm.Host.UOW;
using FluentMigrator.Runner;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Cmm.Host.Hubs;

namespace Cmm.Host
{
    /// <summary>
    /// Стартап.
    /// </summary>
    public class Startup
    {
        private const string ClientPath = "WebApp";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Параметр.</param>
        /// <param name="env">Параметр.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"); });

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/chathub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });

            app.UseSpa(spa =>
            {
                //spa.Options.SourcePath = ClientPath;
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4300");
                }
            });

            MigrateDb();

            var config = new TypeAdapterConfig();
            TypeAdapterConfig<DeviceEventRequest, DeviceEvent>
                .ForType()
                .Map(dest => dest.Id, src => Guid.NewGuid());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Сервисы.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IDevicesService, DevicesService>();
            services.AddTransient<IDeviceEventService, DeviceEventService>();
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<NotificationHub>();

            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4300")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "CmmApi"
                    });
            });
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(Configuration["connectionString"])
                    .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private void MigrateDb()
        {
            PostgreDbInitializer.CheckAndCreate(Configuration["connectionString"]);

            IServiceProvider serviceProvider = CreateServices();
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
    }
}
