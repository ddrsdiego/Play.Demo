namespace Play.Common.Http.Settings
{
    public class HttpClientsSettings
    {
        public ClientSetting[] Clients { get; set; }
    }

    public class ClientSetting
    {
        public ClientSetting()
        {
            HandlerLifetimeInMinutes = 2;
        }

        public string ServiceName { get; set; }
        public string? BaseAddress { get; set; }

        public int HandlerLifetimeInMinutes { get; init; }

        public ClientEndpoint RequestEndpoints { get; set; }
        public bool IsValidSetting => string.IsNullOrEmpty(ServiceName) || BaseAddress == null;
    }

    public class ClientEndpoint
    {
        public string Path { get; set; }
        public string[] AcceptedHeaders { get; set; }
    }
}