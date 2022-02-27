namespace Play.Catalog.Service.Core.Domain.AggregateModels.ItemModel
{
    using System;
    using Common.SeedWorks;

    public class Item : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public static Item DefaultInstance() => new();
    }
}