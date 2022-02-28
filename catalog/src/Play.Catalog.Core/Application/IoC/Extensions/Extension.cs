namespace Play.Catalog.Core.Application.IoC.Extensions
{
    using Infra.Extensions;
    using Infra.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddUseCases();
            services.AddOptions(configuration);
            services.AddRepositories(configuration);
            return services;
        }
    }
}