namespace Play.Catalog.Core.Application.UseCases.CreateItem.Extensions
{
    using Domain.AggregateModels.ItemModel;

    public static class CreateItemUseCaseRspExtension
    {
        public static CreateItemUseCaseRsp ToResponse(this Item newItem)
        {
            var rsp = new CreateItemUseCaseRsp(newItem.Id, newItem.Name, newItem.Description, newItem.Price,
                newItem.CreatedAt);

            return rsp;
        }
    }
}