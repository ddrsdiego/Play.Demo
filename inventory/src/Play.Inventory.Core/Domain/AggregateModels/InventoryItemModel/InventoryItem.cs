namespace Play.Inventory.Core.Domain.AggregateModels.InventoryItemModel
{
    using System;
    using Common.SeedWorks;

    public class InventoryItem : IEntity
    {
        public InventoryItem()
        {
            Id = Guid.NewGuid().ToString();
            AcquiredAt = DateTimeOffset.Now;
        }

        public string Id { get; }

        public string UserId { get; set; }

        public string CatalogItemId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredAt { get; set; }

        public static InventoryItem Default() => new();
    }
}