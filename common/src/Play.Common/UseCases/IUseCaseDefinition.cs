namespace Play.Common.UseCases
{
    using System.Threading.Tasks;

    public interface IUseCaseDefinition<in TRequest>
        where TRequest : IUseCaseRequest
    {
        Task<Response> Execute(TRequest request);
    }
}