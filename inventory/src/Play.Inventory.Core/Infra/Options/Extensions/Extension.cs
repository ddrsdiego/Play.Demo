namespace Play.Inventory.Core.Infra.Options
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(options => configuration.GetSection(nameof(AppSettings)).Bind(options));
            services.Configure<HttpClientsSettings>(options =>
                configuration.GetSection(nameof(HttpClientsSettings)).Bind(options));
            return services;
        }
    }
}