namespace Play.Catalog.Core.Application.UseCases.UpdateItem
{
    using System;
    using Common.UseCases;

    public readonly struct UpdateItemUseCaseReq : IUseCaseRequest
    {
        public UpdateItemUseCaseReq(string id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            RequestId = Guid.NewGuid().ToString();
        }

        public string RequestId { get; }
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
    }
}