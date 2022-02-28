namespace Play.Catalog.Core.Application.IoC.Extensions
{
    using Domain.AggregateModels.ItemModel;
    using Microsoft.Extensions.DependencyInjection;
    using UseCases.CreateItem;
    using UseCases.UpdateItem;

    internal static class UseCasesContainer
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services
                .AddScoped<IUseCaseDefinition<CreateItemUseCaseReq, CreateItemUseCaseRsp>,
                    CreateItemUseCaseImpl>();
            services
                .AddScoped<IUseCaseDefinition<UpdateItemUseCaseReq, UpdateItemUseCaseRsp>,
                    UpdateItemUseCaseImpl>();

            return services;
        }
    }
}