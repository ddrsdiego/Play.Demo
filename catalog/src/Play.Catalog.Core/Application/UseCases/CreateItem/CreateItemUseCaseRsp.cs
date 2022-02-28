namespace Play.Catalog.Core.Application.UseCases.CreateItem
{
    using System;
    using Domain.AggregateModels.ItemModel;

    public record CreateItemUseCaseRsp(string Id, string Name, string Description, decimal Price,
        DateTimeOffset CreatedAt) : IUseCaseResponse;
}