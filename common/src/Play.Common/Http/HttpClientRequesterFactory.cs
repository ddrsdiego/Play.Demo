namespace Play.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Net.Http;

    public class HttpClientRequesterFactory : IHttpClientRequesterFactory
    {
        private string _serviceName;
        private ImmutableDictionary<string, object> _routeParamters;

        private readonly IHttpClientFactory _httpClientFactory;
        
        public HttpClientRequesterFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _routeParamters = ImmutableDictionary<string, object>.Empty;
        }
        
        public IHttpClientRequesterFactory WithServiceName(string serviceName)
        {
            _serviceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
            return this;
        }

        public IHttpClientRequesterFactory WithRoutParameter(string parameterName, object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));

            if (!_routeParamters.TryAdd(parameterName, value))
                throw new Exception();

            return this;
        }

        public IHttpClientRequester CreateGetRequest<TResponse>()
        {
            throw new System.NotImplementedException();
        }

        public IHttpClientRequester ForPost()
        {
            throw new System.NotImplementedException();
        }
    }
}