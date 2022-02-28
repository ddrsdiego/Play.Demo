namespace Play.Inventory.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.MongoDB.Settings;
    using Core.Application.Requests;
    using Core.Application.Responses;
    using Core.Domain.AggregateModels.InventoryItemModel;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMongoRepository<InventoryItem> _repository;

        public ItemsController(IMongoRepository<InventoryItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemResponse>>> GetAll()
        {
            var items = (await _repository.GetAll()).Select(x => x.AsResponse());
            return Ok(items);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<IEnumerable<InventoryItemResponse>>> GetByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var items = (await _repository.GetAll(
                    x => x.UserId == userId))
                .Select(x => x.AsResponse());

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GrantItemRequest request)
        {
            var inventoryItem = await TryGetInventoryItem();

            if (inventoryItem is null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = request.CatalogItemId,
                    UserId = request.UserId,
                    AcquiredAt = DateTimeOffset.Now,
                    Quantity = request.Quantity
                };

                await _repository.Create(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += request.Quantity;
                await _repository.Update(inventoryItem);
            }

            return Ok();

            async Task<InventoryItem?> TryGetInventoryItem()
            {
                var inventoryItem = await _repository.Get(item =>
                    item.CatalogItemId == request.CatalogItemId
                    && item.UserId == request.UserId);
                return inventoryItem;
            }
        }
    }
}