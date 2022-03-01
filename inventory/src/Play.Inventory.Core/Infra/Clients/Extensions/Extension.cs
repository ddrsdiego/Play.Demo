namespace Play.Inventory.Core.Infra.Clients
{
    using Common.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCommonClients(configuration);
            services.AddSingleton<ICatalogClient, CatalogClient>();

            return services;
        }
    }
}