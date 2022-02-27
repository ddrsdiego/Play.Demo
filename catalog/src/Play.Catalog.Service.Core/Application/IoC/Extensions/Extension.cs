namespace Play.Catalog.Service.Core.Application.IoC.Extensions
{
    using Infra.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Play.Catalog.Service.Core.Infra.Options.Extensions;

    public static class Extension
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.AddRepositories(configuration);

            return services;
        }
    }
}