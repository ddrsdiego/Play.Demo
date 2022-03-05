namespace Play.Common.Http
{
    using System.Threading.Tasks;

    public interface IHttpClientRequester
    {
        Task Call();
    }
}