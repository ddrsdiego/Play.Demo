namespace Play.Inventory.Core.Application.IoC
{
    using Common.MongoDB;
    using Domain.AggregateModels.InventoryItemModel;
    using Infra.Clients;
    using Infra.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddInventoryServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            services
                .AddMongo(appSettings.AppName)
                .AddMongoRepository<InventoryItem>("inventory-items");
            services.AddClients(configuration);

            return services;
        }
    }
}