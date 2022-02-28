namespace Play.Common.MongoDB
{
    using System;
    using global::MongoDB.Bson;
    using global::MongoDB.Bson.Serialization;
    using global::MongoDB.Bson.Serialization.Serializers;
    using global::MongoDB.Driver;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using SeedWorks;
    using Settings;

    public static class MongoContainer
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, string serviceName)
        {
            if (serviceName == null) throw new ArgumentNullException(nameof(serviceName));

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var mongoSettings = config.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
                var mongoClient = new MongoClient(mongoSettings.ConnectionString);

                return mongoClient.GetDatabase(serviceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
            where T : IEntity
        {
            services.TryAddSingleton<IMongoRepository<T>>(sp =>
            {
                var dataBase = sp.GetRequiredService<IMongoDatabase>();
                var logger = sp.GetRequiredService<ILogger<MongoRepository<T>>>();

                return new MongoRepository<T>(logger, dataBase, collectionName);
            });

            return services;
        }
    }
}