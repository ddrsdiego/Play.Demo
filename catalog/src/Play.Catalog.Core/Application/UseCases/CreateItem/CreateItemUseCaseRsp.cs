namespace Play.Catalog.Core.Application.UseCases.CreateItem
{
    using System;
    using System.Text.Json.Serialization;

    public readonly struct CreateItemUseCaseRsp
    {
        public readonly string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public DateTimeOffset CreatedAt { get; }

        [JsonConstructor]
        public CreateItemUseCaseRsp(string id, string name, string description, decimal price,
            DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
        }
    }
}