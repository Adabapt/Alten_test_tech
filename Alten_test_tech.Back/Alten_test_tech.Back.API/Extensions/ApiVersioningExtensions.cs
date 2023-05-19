using Microsoft.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// API Versioning class extensions
    /// </summary>
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// Extension API versioning via URL Segment
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(_options =>
            {
                _options.GroupNameFormat = "'v'VVV";
                _options.SubstituteApiVersionInUrl = true;
            }).AddApiVersioning(_config =>
            {
                // There are 4 predefined version selectors in Microsoft.Web.Http.Versioning namespace:
                // CurrentImplementationApiVersionSelector selects the latest api version if none is specified in request.
                // LowestImplementedApiVersionSelector selects the lowest api version if none is specified in request.
                // ConstantApiVersionSelector selects constant api version passed in constructor if none is specified in request.
                // DefaultApiVersionSelector selects DefaultApiVersion in ApiVersioningOptions if none is specified in request.

                // Si non specifié, on utilise la defaut
                _config.AssumeDefaultVersionWhenUnspecified = false;

                // Versioning utilise l'Url Segment /api/v1.0/controller
                _config.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader()
                );
                
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                _config.ReportApiVersions = true;
            });

            return services;
        }
    }
}
