namespace Play.Catalog.Core.Application.Requests
{
    public record CreateItemRequest(string Name, string Description, decimal Price);
}