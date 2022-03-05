namespace Play.Inventory.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Http;
    using Common.MongoDB.Settings;
    using Core.Application.Requests;
    using Core.Application.Responses;
    using Core.Domain.AggregateModels.InventoryItemModel;
    using Core.Infra.Clients;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ICatalogClient _catalogClient;
        private readonly IHttpClientRequesterFactory _httpClientRequesterFactory;
        private readonly IMongoRepository<InventoryItem> _repository;

        public ItemsController(IMongoRepository<InventoryItem> repository,
            ICatalogClient catalogClient,
            IHttpClientRequesterFactory httpClientRequesterFactory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _catalogClient = catalogClient ?? throw new ArgumentNullException(nameof(catalogClient));
            _httpClientRequesterFactory = httpClientRequesterFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemResponse>>> GetAll()
        {
            var catalogItems = await _catalogClient.GetCatalogItems();
            var inventoryItem = await _repository.GetAll();

            var items = inventoryItem.Select(inventoryItem =>
            {
                var (_, name, description) = catalogItems.Single(x => x.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsResponse(name, description);
            });
            return Ok(items);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<IEnumerable<InventoryItemResponse>>> GetByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var requester = _httpClientRequesterFactory
                .WithServiceName("CatalogClient")
                .WithRoutParameter("id", "e5afa751-0298-43f6-8db7-da279610a03c")
                .CreateGetRequest<string>();

           
            var catalogItems = await _catalogClient.GetCatalogItems();
            var inventoryItems = await _repository.GetAll(x => x.UserId == userId);

            var items = inventoryItems
                .Select(inventoryItem =>
                {
                    var (_, name, description) = catalogItems.Single(x => x.Id == inventoryItem.CatalogItemId);
                    return inventoryItem.AsResponse(name, description);
                });

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GrantItemRequest request)
        {
            var inventoryItem = await _repository
                .Get(item => item.CatalogItemId == request.CatalogItemId && item.UserId == request.UserId);

            if (inventoryItem is null)
            {
                inventoryItem = new InventoryItem
                {
                    UserId = request.UserId,
                    CatalogItemId = request.CatalogItemId,
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
        }
    }
}