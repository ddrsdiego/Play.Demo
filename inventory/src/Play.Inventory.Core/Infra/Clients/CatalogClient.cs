namespace Play.Inventory.Core.Infra.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Application.Responses;
    using Common.Http;

    public interface ICatalogClient
    {
        Task<IReadOnlyCollection<CatalogItemResponse>> GetCatalogItems();
    }

    public sealed class CatalogClient : CommonClient, ICatalogClient
    {
        public CatalogClient(IHttpClientFactory clientFactory)
            : base(clientFactory, nameof(CatalogClient))
        {
        }

        public async Task<IReadOnlyCollection<CatalogItemResponse>> GetCatalogItems()
        {
            try
            {
                var httpClient = _clientFactory.CreateClient(ServiceName);

                var items = await httpClient
                    .GetFromJsonAsync<IReadOnlyCollection<CatalogItemResponse>>("items");

                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}