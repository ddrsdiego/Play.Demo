namespace Play.Catalog.Service.Core.Infra.Extensions
{
    using Domain.AggregateModels.ItemModel;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;

    internal static class RepositoriesContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            
            services
                .AddMongo(appSettings.AppName)
                .AddMongoRepository<Item>("items");

            return services;
        }
    }
}