namespace Play.Catalog.Service.Core.Infra.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Domain.SeedWorkds;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver;
    using Options;
    using Repositories;

    public class MongoRepositorySettings
    {
        public MongoRepositorySettings()
        {
            Items = ImmutableDictionary<string, Type>.Empty;
        }

        public ImmutableDictionary<string, Type> Items { get; }

        public void Add<TEntity>(string collectionName)
        {
            Items.TryAdd(collectionName, typeof(TEntity));
        }
    }

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

        public static IServiceCollection AddMongo(this IServiceCollection services, string serviceName,
            Action<MongoRepositorySettings> specification)
        {
            if (serviceName == null) throw new ArgumentNullException(nameof(serviceName));
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            var mongoRepositorySettings = new MongoRepositorySettings();
            specification(new MongoRepositorySettings());

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
                return new MongoRepository<T>(dataBase, collectionName);
            });

            return services;
        }
    }
}