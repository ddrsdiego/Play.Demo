namespace Play.Catalog.Core.Application.UseCases.UpdateItem
{
    using System.Threading.Tasks;
    using Common;
    using Domain.AggregateModels.ItemModel;

    public class UpdateItemUseCaseImpl : IUseCaseDefinition<UpdateItemUseCaseReq>
    {
        public Task<Response> Execute(UpdateItemUseCaseReq request)
        {
            throw new System.NotImplementedException();
        }
    }
}