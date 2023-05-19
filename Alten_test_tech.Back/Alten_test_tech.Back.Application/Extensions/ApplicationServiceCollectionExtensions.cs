using AutoMapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Alten_test_tech.Back.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// La méthode d'extension <see cref="IServiceCollection"/> ajoute les services de l'application layer
    /// </summary>
    /// <remarks>
    /// AddSingleton - Seulement une instance est créé et retourné.
    /// AddScoped - Une instance pour chaque cycle de request/response.
    /// AddTransient - Une nouvelle instance est créer à chaque fois
    /// </remarks>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Ajoute les services de l'application layer
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            
            _ = services.AddScoped<IProductService, ProductService>();

            _ = services.AddMediatR(typeof(DomainServiceCollectionExtensions).Assembly);
            _ = services.AddApplicationMappers();

            return services;
        }

        public static IServiceCollection AddApplicationMappers(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}
