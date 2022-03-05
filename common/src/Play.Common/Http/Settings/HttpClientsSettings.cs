namespace Play.Common.Http.Settings
{
    public class HttpClientsSettings
    {
        public ClientSetting[] Clients { get; set; }
    }

    public class ClientSetting
    {
        /// <summary>
        /// Default value of 1 second for request timeout
        /// </summary>
        public const int DefaultTimeoutInSeconds = 1;

        /// <summary>
        /// Default value of 2 minutes to keep an HttpClient instance in the pool until the next instance.
        /// </summary>
        public const int DefaultHandlerLifetimeInMinutes = 2;

        public ClientSetting()
        {
            TimeoutInSeconds = DefaultTimeoutInSeconds;
            HandlerLifetimeInMinutes = DefaultHandlerLifetimeInMinutes;
        }

        public string ServiceName { get; set; }
        public string? BaseAddress { get; set; }
        public int HandlerLifetimeInMinutes { get; init; }
        public int TimeoutInSeconds { get; set; }

        public ClientEndpoint[] RequestEndpoints { get; set; }
        
        public bool IsInvalidSetting => !IsValidSetting;
        
        public bool IsValidSetting => !string.IsNullOrEmpty(ServiceName) && BaseAddress != null;
    }

    public class ClientEndpoint
    {
        public string Path { get; set; }
        public string[] AcceptedHeaders { get; set; }
        public ClientEndpointRouteParameter[] RouteParameters { get; set; }
    }

    public class ClientEndpointRouteParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}