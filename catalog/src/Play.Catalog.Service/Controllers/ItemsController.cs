namespace Play.Catalog.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catalog.Core.Application.Requests;
    using Catalog.Core.Application.Requests.Extensions;
    using Catalog.Core.Application.Responses;
    using Catalog.Core.Application.Responses.Extensions;
    using Catalog.Core.Application.UseCases.CreateItem;
    using Catalog.Core.Application.UseCases.UpdateItem;
    using Catalog.Core.Domain.AggregateModels.ItemModel;
    using Common.MongoDB.Settings;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMongoRepository<Item> _itemRepository;
        private readonly IUseCaseDefinition<CreateItemUseCaseReq> _createItemUseCase;
        private readonly IUseCaseDefinition<UpdateItemUseCaseReq> _updateItemUseCase;

        private static int requestCounter = 0;

        public ItemsController(IMongoRepository<Item> itemRepository,
            IUseCaseDefinition<CreateItemUseCaseReq> createItemUseCase,
            IUseCaseDefinition<UpdateItemUseCaseReq> updateItemUseCase)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _createItemUseCase = createItemUseCase ?? throw new ArgumentNullException(nameof(createItemUseCase));
            _updateItemUseCase = updateItemUseCase ?? throw new ArgumentNullException(nameof(updateItemUseCase));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemResponse>>> Get()
        {
            requestCounter++;
            Console.WriteLine($"Request {requestCounter}: Starting...");

            if (requestCounter % 4 == 0)
            {
                Console.WriteLine($"Request {requestCounter}: Delaying...");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            if (requestCounter % 5 == 0)
            {
                Console.WriteLine($"Request {requestCounter}: 500 (Internal Server Error).");
                return StatusCode(500);
            }

            var responses = (await _itemRepository.GetAll()).Select(x => x.AsResponse());

            Console.WriteLine($"Request {requestCounter}: 200 (OK).");
            return Ok(responses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemResponse>> GetById(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var item = await _itemRepository.Get(x => x.Id == id);
            if (item is null)
                return NotFound();

            return Ok(item.AsResponse());
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse>> Post([FromBody] CreateItemRequest request)
        {
            var response = await _createItemUseCase.Execute(request.AsUseCaseRequest());
            var createUseRsp = response.Content.GetRaw<CreateItemUseCaseRsp>();
            
            return CreatedAtAction(nameof(GetById), new { id = createUseRsp.Id }, createUseRsp);
        }
    }
}