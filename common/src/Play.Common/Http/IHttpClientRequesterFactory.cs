namespace Play.Common.Http
{
    public interface IHttpClientRequesterFactory
    {
        IHttpClientRequesterFactory WithServiceName(string serviceName);
        IHttpClientRequesterFactory WithRoutParameter(string parameterName, object value);
        IHttpClientRequester CreateGetRequest<TResponse>();
        IHttpClientRequester ForPost();
    }
}