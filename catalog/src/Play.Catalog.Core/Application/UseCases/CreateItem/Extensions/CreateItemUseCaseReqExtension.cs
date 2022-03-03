namespace Play.Catalog.Core.Application.UseCases.CreateItem.Extensions
{
    using System;
    using Domain.AggregateModels.ItemModel;

    public static class CreateItemUseCaseReqExtension
    {
        public static Item ToNewItem(this CreateItemUseCaseReq req)
        {
            var newItem = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                CreatedAt = DateTimeOffset.Now
            };

            return newItem;
        }
    }
}