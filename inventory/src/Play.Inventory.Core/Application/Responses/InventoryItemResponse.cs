namespace Play.Inventory.Core.Application.Responses
{
    public record InventoryItemResponse(
        string UserId,
        string CatalogItemId,
        string Name,
        string Description,
        int Quantity
    );
}