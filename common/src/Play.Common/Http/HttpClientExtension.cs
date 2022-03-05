namespace Play.Common.Http
{
    using System;
    using System.Collections.Immutable;
    using System.Net.Http;
    using System.Net.Mime;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Polly;
    using Settings;

    public interface IHttpClientsSettingContext
    {
        void AddSetting(string serviceName, ClientSetting clientSetting);

        bool TryGetSetting(string serviceName, out ClientSetting clientSetting);
    }

    internal sealed class HttpClientsSettingContext : IHttpClientsSettingContext
    {
        private ImmutableDictionary<string, ClientSetting> _clients;

        public HttpClientsSettingContext()
        {
            _clients = ImmutableDictionary<string, ClientSetting>.Empty;
        }

        public void AddSetting(string serviceName, ClientSetting clientSetting)
        {
            if (serviceName == null) throw new ArgumentNullException(nameof(serviceName));
            if (clientSetting == null) throw new ArgumentNullException(nameof(clientSetting));


            if (!_clients.TryGetValue(serviceName, out var _))
                _clients = _clients.Add(serviceName, clientSetting);
        }

        public bool TryGetSetting(string serviceName, out ClientSetting clientsSettings)
        {
            throw new NotImplementedException();
        }
    }

    public static class HttpClientExtension
    {
        public static IServiceCollection AddCommonClients(this IServiceCollection services,
            IConfiguration configuration)
        {
            var httpClientsSettings = configuration.GetSection(nameof(HttpClientsSettings)).Get<HttpClientsSettings>();

            foreach (var clientSetting in httpClientsSettings.Clients)
            {
                if (clientSetting.IsInvalidSetting)
                    continue;

                services.AddHttpClient(clientSetting.ServiceName, client =>
                    {
                        client.BaseAddress = new Uri(clientSetting.BaseAddress);
                        client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
                    })
                    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(clientSetting.TimeoutInSeconds))
                    .SetHandlerLifetime(TimeSpan.FromMinutes(clientSetting.HandlerLifetimeInMinutes));
            }

            return services;
        }
    }
}