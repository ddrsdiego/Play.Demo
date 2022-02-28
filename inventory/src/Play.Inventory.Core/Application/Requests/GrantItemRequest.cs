namespace Play.Inventory.Core.Application.Requests
{
    public record GrantItemRequest(
        string CatalogItemId,
        string UserId,
        int Quantity
    );
}