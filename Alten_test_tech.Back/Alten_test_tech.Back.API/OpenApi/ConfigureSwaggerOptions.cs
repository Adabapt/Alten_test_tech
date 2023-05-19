using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;

namespace Alten_test_tech.Back.API.OpenAPI
{
    /// <inheritdoc cref="SwaggerOptions"/>
    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        /// <inheritdoc />
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "{documentName}/swagger.json";

            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                StringValues prefix = this.DeterminePrefix(httpReq);

                StringValues scheme = this.DetermineScheme(httpReq);

                StringValues host = this.DetermineHost(httpReq);

                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = $"{scheme}://{host}{prefix}" },
                };
            });
        }

        private StringValues DetermineHost(HttpRequest httpReq)
        {
            if ( !httpReq.Headers.TryGetValue("X-ORIGINAL-HOST", out var host) )
            {
                host = httpReq.Host.Value;
            }

            return host;
        }

        private StringValues DetermineScheme(HttpRequest httpReq)
        {
            if ( !httpReq.Headers.TryGetValue("X-FORWARDED-PROTO", out var scheme) )
            {
                scheme = httpReq.Scheme;
            }
            return scheme;
        }

        private StringValues DeterminePrefix(HttpRequest httpReq)
        {
            if ( !httpReq.Headers.TryGetValue("X-Forwarded-Prefix", out var prefix) )
            {
                prefix = "";
            }

            if ( !prefix.ToString().StartsWith("/") )
            {
                prefix = $"/{prefix}";
            }

            return prefix;
        }
    }
}
