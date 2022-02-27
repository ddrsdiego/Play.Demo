namespace Play.Catalog.Service.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Application.Requests;
    using Core.Application.Requests.Extensions;
    using Core.Application.Responses;
    using Core.Application.Responses.Extensions;
    using Core.Application.UseCases.CreateItem;
    using Core.Application.UseCases.UpdateItem;
    using Core.Domain.AggregateModels.ItemModel;
    using Core.Infra.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMongoRepository<Item> _itemRepository;
        private readonly IUseCaseDefinition<CreateItemUseCaseReq, CreateItemUseCaseRsp> _createItemUseCase;
        private readonly IUseCaseDefinition<UpdateItemUseCaseReq, UpdateItemUseCaseRsp> _updateItemUseCase;

        public ItemsController(IMongoRepository<Item> itemRepository,
            IUseCaseDefinition<CreateItemUseCaseReq, CreateItemUseCaseRsp> createItemUseCase,
            IUseCaseDefinition<UpdateItemUseCaseReq, UpdateItemUseCaseRsp> updateItemUseCase)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _createItemUseCase = createItemUseCase ?? throw new ArgumentNullException(nameof(createItemUseCase));
            _updateItemUseCase = updateItemUseCase ?? throw new ArgumentNullException(nameof(updateItemUseCase));
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
            var response = await _createItemUseCase.Execute(request.AsUseCaseRequest());
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
    }
}