namespace Play.Catalog.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Application.Requests;
    using Core.Application.Responses;
    using Core.Application.Responses.Extensions;
    using Core.Domain.AggregateModels.ItemModel;
    using Core.Infra.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMongoRepository<Item> _itemRepository;

        public ItemsController(IMongoRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        [HttpGet]
        public async Task<ActionResult<ItemResponse>> Get()
        {
            var responses = (await _itemRepository.Get()).Select(x => x.AsResponse());
            return Ok(responses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemResponse>> GetById(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var item = await _itemRepository.GetById(id);

            return Ok(item.AsResponse());
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse>> Post([FromBody] CreateItemRequest request)
        {
            var newItem = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAt = DateTimeOffset.Now
            };

            await _itemRepository.Create(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }
    }
}