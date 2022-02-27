namespace Play.Catalog.Service.Core.Application.Requests.Extensions
{
    using UseCases.CreateItem;

    public static class Extension
    {
        public static CreateItemUseCaseReq AsUseCaseRequest(this CreateItemRequest request) =>
            new(request.Name, request.Name, request.Price);
    }
}