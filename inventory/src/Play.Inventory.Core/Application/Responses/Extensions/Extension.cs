namespace Play.Inventory.Core.Application.Responses
{
    using Domain.AggregateModels.InventoryItemModel;

    public static class Extension
    {
        public static InventoryItemResponse AsResponse(this InventoryItem inventoryItem, string name,
            string description)
        {
            return new InventoryItemResponse(
                inventoryItem.UserId,
                inventoryItem.CatalogItemId,
                name,
                description,
                inventoryItem.Quantity
            );
        }
    }
}