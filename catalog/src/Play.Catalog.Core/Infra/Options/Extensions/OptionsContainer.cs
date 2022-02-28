namespace Play.Catalog.Core.Infra.Options.Extensions
{
    using Common.MongoDB.Settings;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(options => configuration.GetSection(nameof(AppSettings)).Bind(options));
            services.Configure<MongoSettings>(options => configuration.GetSection(nameof(MongoSettings)).Bind(options));

            return services;
        }
    }
}