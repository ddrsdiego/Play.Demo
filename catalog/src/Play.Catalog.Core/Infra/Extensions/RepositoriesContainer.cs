namespace Play.Catalog.Core.Infra.Extensions
{
    using Common.MongoDB;
    using Domain.AggregateModels.ItemModel;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;

    public static class RepositoriesContainer
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