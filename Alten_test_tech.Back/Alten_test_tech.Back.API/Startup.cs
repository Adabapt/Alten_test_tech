using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using Alten_test_tech.Back.Application;
using Alten_test_tech.Back.Domain;
using Alten_test_tech.Back.Infrastructure;

using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;


[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace Alten_test_tech.Back.API
{
    /// <summary>
    /// The <see cref="Startup"/> class configures services and the app's request pipeline
    /// </summary>
    public class Startup
    {

        internal class Constants
        {
            public const string CorsOrigins = "Cors:Origins";
        }
        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Utilisé pour url redict
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                   ForwardedHeaders.XForwardedFor |
                   ForwardedHeaders.XForwardedHost |
                   ForwardedHeaders.XForwardedProto;
            });

            services.AddControllers();

            services.AddCustomApiVersioning();

            services.AddCustomOpenApi();

            services.AddHealthChecks()
                        .AddCheck("self", () => HealthCheckResult.Healthy());

            //
            services.AddApplicationServiceCollection(this.Configuration)
                .AddInfrastuctureServices(this.Configuration)
                .AddInfrastuctureRepository(this.Configuration)
                .AddDomainCommands();

            string[] clients = this.Configuration.GetSection(Constants.CorsOrigins).Get<string[]>();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddLogging(c => c.ClearProviders());

            services.AddCors(options => options.AddPolicy("AllowCors",
                builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .WithOrigins(clients)
                        ;
                    ;
                })
            );
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowCors");

            // Utilisation d'un proxy
            app.UseForwardedHeaders();

            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
