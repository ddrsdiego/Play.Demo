namespace Play.Catalog.Core.Application.UseCases.UpdateItem
{
    using System.Threading.Tasks;
    using Domain.AggregateModels.ItemModel;

    public class UpdateItemUseCaseImpl : IUseCaseDefinition<UpdateItemUseCaseReq, UpdateItemUseCaseRsp>
    {
        public Task<UpdateItemUseCaseRsp> Execute(UpdateItemUseCaseReq request)
        {
            throw new System.NotImplementedException();
        }
    }
}