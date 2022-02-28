namespace Play.Inventory.Core.Application.Responses
{
    using System;

    public record InventoryItemResponse(
        string UserId,
        string CatalogItemId,
        string Name,
        string Description,
        int Quantity
    );
}