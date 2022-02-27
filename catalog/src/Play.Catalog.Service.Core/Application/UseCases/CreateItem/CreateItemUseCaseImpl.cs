namespace Play.Catalog.Service.Core.Application.UseCases.CreateItem
{
    using System;
    using System.Threading.Tasks;
    using Domain.AggregateModels.ItemModel;
    using Infra.Repositories;

    public class CreateItemUseCaseImpl : IUseCaseDefinition<CreateItemUseCaseReq, CreateItemUseCaseRsp>
    {
        private readonly IMongoRepository<Item> _itemRepository;

        public CreateItemUseCaseImpl(IMongoRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task<CreateItemUseCaseRsp> Execute(CreateItemUseCaseReq req)
        {
            try
            {
                var newItem = new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = req.Name,
                    Description = req.Description,
                    Price = req.Price,
                    CreatedAt = DateTimeOffset.Now
                };

                await _itemRepository.Create(newItem);

                return new CreateItemUseCaseRsp(newItem.Id, newItem.Name, newItem.Description, newItem.Price,
                    newItem.CreatedAt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}