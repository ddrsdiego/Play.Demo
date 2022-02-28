namespace Play.Catalog.Core.Application.UseCases.CreateItem
{
    using System;
    using Domain.AggregateModels.ItemModel;

    public class CreateItemUseCaseReq : IUseCaseRequest
    {
        public CreateItemUseCaseReq(string name, string description, decimal price)
            : this(Guid.NewGuid().ToString(), name, description, price)
        {
        }

        public CreateItemUseCaseReq(string requestId, string name, string description, decimal price)
        {
            RequestId = requestId;
            Name = name;
            Description = description;
            Price = price;
        }

        public string RequestId { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
    }
}