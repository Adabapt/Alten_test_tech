using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Linq;

namespace Alten_test_tech.Back.API.OpenAPI
{
    /// <summary>
    /// Filtre les paramètres d'une opération
    /// </summary>
    public class RemoveVersionFromParameter : IOperationFilter
    {
        /// <summary>
        /// Applique le filtre sur l'opération
        /// </summary>
        /// <param name="operation">Opération</param>
        /// <param name="context">Context</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
