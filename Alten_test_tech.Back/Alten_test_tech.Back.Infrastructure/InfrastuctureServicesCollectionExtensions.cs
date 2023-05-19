using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Alten_test_tech.Back.Domain;
using Alten_test_tech.Back.Domain.Products;

using Alten_test_tech.Back.Infrastructure.Repositories;


namespace Alten_test_tech.Back.Infrastructure
{
    public static class InfrastuctureServicesCollectionExtensions
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services , IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddInfrastuctureRepository(this IServiceCollection services, IConfiguration configuration)
        {

            _ = services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
