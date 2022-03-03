namespace Play.Catalog.Core.Application.Requests.Extensions
{
    using System;
    using Service.Core.Application.Requests;
    using UseCases.CreateItem;
    using UseCases.UpdateItem;

    public static class Extension
    {
        public static CreateItemUseCaseReq AsUseCaseRequest(this CreateItemRequest request) =>
            new(request.Name, request.Name, request.Price);

        public static UpdateItemUseCaseReq AsUseCaseRequest(this UpdateItemRequest request, string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var (name, description, price) = request;

            var updateItemUseCaseReq = new UpdateItemUseCaseReq(id, name, description, price);
            return updateItemUseCaseReq;
        }
    }
}