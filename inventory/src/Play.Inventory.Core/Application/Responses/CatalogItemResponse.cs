namespace Play.Inventory.Core.Application.Responses
{
    public record CatalogItemResponse(
        string Id,
        string Name,
        string Description);
}