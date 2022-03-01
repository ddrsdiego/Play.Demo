namespace Play.Common.Http
{
    using System;
    using System.Net.Mime;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Settings;

    public static class HttpClientExtension
    {
        public static IServiceCollection AddCommonClients(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClientsSettings = configuration.GetSection(nameof(HttpClientsSettings)).Get<HttpClientsSettings>();

            foreach (var clientSetting in httpClientsSettings.Clients)
            {
                if (string.IsNullOrEmpty(clientSetting.ServiceName) || clientSetting.BaseAddress == null)
                    continue;

                services.AddHttpClient(clientSetting.ServiceName, client =>
                {
                    client.BaseAddress = new Uri(clientSetting.BaseAddress);
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
                });
            }

            return services;
        }
    }
}