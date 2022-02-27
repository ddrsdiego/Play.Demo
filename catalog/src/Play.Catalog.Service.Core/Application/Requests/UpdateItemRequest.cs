namespace Play.Catalog.Service.Core.Application.Requests
{
    public record UpdateItemRequest(string Name, string Description, decimal Price);
}