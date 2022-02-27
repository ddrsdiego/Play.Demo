namespace Play.Common.MongoDB.Settings
{
    public class MongoSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}