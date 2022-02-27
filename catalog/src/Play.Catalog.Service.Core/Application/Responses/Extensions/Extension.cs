namespace Play.Catalog.Service.Core.Application.Responses.Extensions
{
    using Domain.AggregateModels.ItemModel;

    public static class Extension
    {
        public static ItemResponse AsResponse(this Item item) =>
            new(item.Id, item.Name, item.Description, item.Price, item.CreatedAt);
    }
}