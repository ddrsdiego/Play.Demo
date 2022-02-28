namespace Play.Inventory.Core.Application.Responses
{
    using Domain.AggregateModels.InventoryItemModel;

    public static class Extension
    {
        public static InventoryItemResponse AsResponse(this InventoryItem inventoryItem)
        {
            return new InventoryItemResponse(
                inventoryItem.Id,
                inventoryItem.UserId,
                inventoryItem.CatalogItemId,
                inventoryItem.Quantity,
                inventoryItem.AcquiredAt
            );
        }
    }
}