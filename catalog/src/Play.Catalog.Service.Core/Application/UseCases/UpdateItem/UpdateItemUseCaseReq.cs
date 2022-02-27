namespace Play.Catalog.Service.Core.Application.UseCases.UpdateItem
{
    using Domain.AggregateModels.ItemModel;

    public class UpdateItemUseCaseReq : IUseCaseRequest
    {
        public string RequestId { get; }
    }
}