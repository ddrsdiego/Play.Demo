namespace Play.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mime;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Settings;

    internal class RequestHeadersMap
    {
        private const string Json = "json";
        private const string Soap = "soap+xml";

        public RequestHeadersMap(string requestHeaderSetting, string requestHeaderClient)
        {
            RequestHeaderSetting = requestHeaderSetting;
            RequestHeaderClient = requestHeaderClient;
        }

        public readonly string RequestHeaderClient;
        public readonly string RequestHeaderSetting;

        private static readonly RequestHeadersMap SoapXml = new(Soap, MediaTypeNames.Application.Soap);
        private static readonly RequestHeadersMap JsonHeader = new(Json, MediaTypeNames.Application.Json);

        private static IEnumerable<RequestHeadersMap?> GetAll()
        {
            return new[]
            {
                JsonHeader
            };
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
                if (clientSetting.IsValidSetting)
                    continue;

                services.AddHttpClient(clientSetting.ServiceName, client =>
                {
                    client.BaseAddress = new Uri(clientSetting.BaseAddress);
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
                }).SetHandlerLifetime(TimeSpan.FromMinutes(clientSetting.HandlerLifetimeInMinutes));
            }

            return services;
        }
    }
}