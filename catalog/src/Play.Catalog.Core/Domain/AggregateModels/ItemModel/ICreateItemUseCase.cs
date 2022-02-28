namespace Play.Catalog.Core.Domain.AggregateModels.ItemModel
{
    using System.Threading.Tasks;

    public interface IUseCaseRequest
    {
        string RequestId { get; }
    }

    public interface IUseCaseResponse
    {
    }

    public interface IUseCaseDefinition<in TRequest, TResponse>
        where TRequest : IUseCaseRequest
        where TResponse : IUseCaseResponse
    {
        Task<TResponse> Execute(TRequest request);
    }

    


}