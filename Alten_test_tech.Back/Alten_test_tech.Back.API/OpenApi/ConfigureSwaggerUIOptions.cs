using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace Alten_test_tech.Back.API.OpenAPI
{
    /// <inheritdoc cref="SwaggerUIOptions"/>
    internal sealed class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly IConfiguration Configuration;

        /// <inheritdoc />
        public ConfigureSwaggerUIOptions(
            IApiVersionDescriptionProvider versionDescriptionProvider,
            IConfiguration configuration)
        {
            this.provider = versionDescriptionProvider ?? throw new ArgumentNullException(nameof(versionDescriptionProvider));

            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerUIOptions options)
        {
            foreach ( ApiVersionDescription description in this.provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"../{description.GroupName}/swagger.json",
                    $"{description.GroupName.ToUpperInvariant()} (Reverse Proxy)");

                options.SwaggerEndpoint($"/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        }
    }
}
