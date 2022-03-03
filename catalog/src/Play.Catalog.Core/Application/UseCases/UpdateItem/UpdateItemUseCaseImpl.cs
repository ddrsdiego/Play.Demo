namespace Play.Catalog.Core.Application.UseCases.UpdateItem
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Common.Async;
    using Common.MongoDB.Settings;
    using Common.UseCases;
    using Domain.AggregateModels.ItemModel;
    using Microsoft.Extensions.Logging;

    public class UpdateItemUseCaseImpl : UseCaseDefinition<UpdateItemUseCaseReq>
    {
        private readonly IMongoRepository<Item> _repository;

        public UpdateItemUseCaseImpl(ILoggerFactory loggerFactory, IMongoRepository<Item> repository)
            : base(loggerFactory.CreateLogger<UpdateItemUseCaseImpl>())
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<Response> Execute(UpdateItemUseCaseReq request)
        {
            try
            {
                var item = await _repository.Get(x => x.Id == request.Id).FastResult();
                if (item is null)
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Response.Ok();
        }
    }
}