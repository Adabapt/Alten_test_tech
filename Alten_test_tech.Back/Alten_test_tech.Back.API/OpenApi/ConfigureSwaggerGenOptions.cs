using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alten_test_tech.Back.API.OpenAPI
{
    /// <summary>
    /// Configuration Swagger
    /// </summary>
    internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider Provider;
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="configuration">The key/Value application configuration</param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            this.Provider = provider;
            this.Configuration = configuration;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            this.SetXmlCommentsFilePath(options);

            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            foreach ( var description in this.Provider.ApiVersionDescriptions )
            {
                options.SwaggerDoc(description.GroupName, this.CreateInfoForApiVersion(description));
            }
        }


        /// <summary>
        /// CreateInfoForApiVersion Swagger
        /// </summary>
        /// <param name="aDescription">Représente la description de l'API</param>
        /// <returns><see cref="OpenApiInfo"/> object</returns>
        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription aDescription)
        {
            var info = new OpenApiInfo()
            {
                Title = $"Alten_test_tech.Back.API {aDescription.ApiVersion}",
                Version = aDescription.ApiVersion.ToString(),
                Description = "",
                Contact = new OpenApiContact() {
                    Name = "Bapt ",
                },
            };

            if (aDescription.IsDeprecated)
            {
                StringBuilder warningMD = new StringBuilder()
                    .Append("Use of the API is considered to be unsafe")
                    .Append(" or an improved alternative API has been made available,")
                    .Append(" or breaking changes to the API are expected in a future major release.");

                info.Description = $"{info.Description} - **APIs DEPRECATED** {warningMD}";
            }

            return info;
        }

        /// <summary>
        /// Fichier Xml de commentaire
        /// </summary>
        /// <returns>Retourne le chemin vers le fichier Xml de commentaire</returns>
        private void SetXmlCommentsFilePath(SwaggerGenOptions options)
        {
            var basePath = AppContext.BaseDirectory;
            var fileName = $"{typeof(ConfigureSwaggerGenOptions).GetTypeInfo().Assembly.GetName().Name}.xml";

            var path = Path.Combine(basePath, fileName);
            if ( File.Exists(path) )
            {
                options.IncludeXmlComments(path);
            }
        }

    }
}
