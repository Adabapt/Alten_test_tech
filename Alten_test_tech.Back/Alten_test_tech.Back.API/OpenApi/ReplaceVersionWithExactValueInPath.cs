using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Linq;

namespace Alten_test_tech.Back.API.OpenAPI
{
    /// <summary>
    /// Replace parameter 'api/v{version}/' present in 
    /// Remplace le paramètre 'api/v{version}/' présent dans 
    /// </summary>
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openApiDocument">L'object OpenApi</param>
        /// <param name="context">Context</param>
        public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
        {
            var newPaths = openApiDocument.Paths
                .ToDictionary(
                    path => path.Key.Replace("v{version}", $"v{openApiDocument.Info.Version}"),
                    path => path.Value
                );

            openApiDocument.Paths = new OpenApiPaths();
            foreach ((string key, OpenApiPathItem value) in newPaths)
            {
                openApiDocument.Paths[key] = value;
            }
        }
    }
}
