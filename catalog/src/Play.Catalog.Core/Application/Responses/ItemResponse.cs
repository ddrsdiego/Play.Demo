namespace Play.Catalog.Core.Application.Responses
{
    using System;

    public record ItemResponse(string Id, string Name, string Description, decimal Price, DateTimeOffset CreatedAt);
}