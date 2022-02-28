namespace Play.Inventory.Core.Infra.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Application.Responses;
    using Common.Async;

    public sealed class CatalogClient
    {
        private readonly HttpClient _httpClient;

        public const string ServiceName = nameof(CatalogClient);
        
        public CatalogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ValueTask<IReadOnlyCollection<CatalogItemResponse>> GetCatalogItems()
        {
            try
            {
                var items = _httpClient
                    .GetFromJsonAsync<IReadOnlyCollection<CatalogItemResponse>>("items")
                    .FastResult();

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