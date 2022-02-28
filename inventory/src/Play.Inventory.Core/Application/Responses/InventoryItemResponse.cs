namespace Play.Inventory.Core.Application.Responses
{
    using System;

    public record InventoryItemResponse(
        string Id,
        string UserId,
        string CatalogItemId,
        int Quantity,
        DateTimeOffset AcquiredAt
    );
}