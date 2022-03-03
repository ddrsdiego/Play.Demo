namespace Play.Catalog.Core.Application.UseCases.UpdateItem
{
    using Common.UseCases;
    using Domain.AggregateModels.ItemModel;

    //
    // public class UpdateItemUseCaseReq : IUseCaseRequest
    // {
    //     public string RequestId { get; }
    // }

    public record UpdateItemUseCaseReq(
        string RequestId,
        string Id,
        string Name,
        string Description,
        decimal Price
    ) : IUseCaseRequest;
}