namespace Play.Common.Http.Settings
{
    public class HttpClientsSettings
    {
        public ClientSetting[] Clients { get; set; }
    }

    public class ClientSetting
    {
        public string ServiceName { get; set; }
        public string? BaseAddress { get; set; }
    }
}