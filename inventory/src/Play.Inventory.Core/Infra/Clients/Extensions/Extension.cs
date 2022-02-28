namespace Play.Inventory.Core.Infra.Clients
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Options;

    public static class Extension
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClientsSettings = configuration.GetSection(nameof(HttpClientsSettings)).Get<HttpClientsSettings>();

            services.AddHttpClient<CatalogClient>(ConfigureClient(httpClientsSettings, CatalogClient.ServiceName));

            return services;
        }

        private static Action<HttpClient> ConfigureClient(HttpClientsSettings httpClientsSettings, string serviceName) =>
            builder =>
            {
                var clientSetting =
                    httpClientsSettings.Clients.First(x => x.ServiceName == serviceName);

                if (clientSetting.BaseAddress != null)
                    builder.BaseAddress = new Uri(clientSetting.BaseAddress);
            };
    }
}