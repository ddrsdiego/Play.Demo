namespace Play.Common.Http
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Settings;

    public class HttpClientRequester : IHttpClientRequester
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpClientsSettingContext _httpClientsSettingContext;

        public HttpClientRequester(IHttpClientFactory httpClientFactory,
            IHttpClientsSettingContext httpClientsSettingContext)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientsSettingContext = httpClientsSettingContext;
        }

        public async Task<T> Get<T>(string serviceName, string id)
        {
            if (serviceName == null) throw new ArgumentNullException(nameof(serviceName));

            ClientSetting clientSetting;
            if (!_httpClientsSettingContext.TryGetSetting(serviceName, out clientSetting))
                throw new Exception();

            var httpClient = _httpClientFactory.CreateClient(clientSetting.ServiceName);
            
            await Task.CompletedTask;

            return default(T);
        }

        public Task Call()
        {
            throw new NotImplementedException();
        }
    }
}