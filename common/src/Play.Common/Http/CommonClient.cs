namespace Play.Common.Http
{
    using System;
    using System.Net.Http;

    public abstract class CommonClient
    {
        protected readonly IHttpClientFactory _clientFactory;

        protected CommonClient(IHttpClientFactory clientFactory, string serviceName)
        {
            ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        protected string ServiceName { get; }
    }
}