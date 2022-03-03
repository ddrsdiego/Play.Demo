namespace Play.Catalog.Service.IoC
{
    using Catalog.Core.Application.UseCases.CreateItem;
    using Catalog.Core.Infra.Extensions;
    using Catalog.Core.Infra.Options;
    using Common.UseCases.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddUseCases(typeof(CreateItemUseCaseImpl).Assembly);
            services.AddOptions(configuration);
            services.AddRepositories(configuration);
            return services;
        }
    }
}