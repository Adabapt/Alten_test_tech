using Microsoft.Extensions.Options;

using Alten_test_tech.Back.API.OpenAPI;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Classe d'extension OpenApi
    /// </summary>
    public static class OpenApiExtensions
    {
        /// <summary>
        /// Ajoute les services OpenApi / Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Collection de services</returns>
        public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
                .AddTransient<IConfigureOptions<SwaggerOptions>, ConfigureSwaggerOptions>()
                .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>()
                .AddSwaggerGen();

            return services;
        }
    }
}
