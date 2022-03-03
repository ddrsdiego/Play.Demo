namespace Play.Catalog.Core.Application.UseCases.CreateItem
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Common.MongoDB.Settings;
    using Domain.AggregateModels.ItemModel;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    public sealed class CreateItemUseCaseImpl : IUseCaseDefinition<CreateItemUseCaseReq>
    {
        private readonly IMongoRepository<Item> _itemRepository;

        public CreateItemUseCaseImpl(IMongoRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task<Response> Execute(CreateItemUseCaseReq request)
        {
            Response response;

            try
            {
                var newItem = request.ToNewItem();
                await _itemRepository.Create(newItem);

                response = Response.Ok(ResponseContent.Create(newItem.ToResponse()), StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                response = Response.Fail(new Error("", ""), StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}