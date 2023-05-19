namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainServiceCollectionExtensions
    {
        /// <summary>
        /// Ajoute collection de service concernant le domain
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainCommands(this IServiceCollection services)
        {
            return services;
        }
    }
}
