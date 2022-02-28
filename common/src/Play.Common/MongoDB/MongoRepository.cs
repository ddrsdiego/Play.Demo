namespace Play.Common.MongoDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Async;
    using global::MongoDB.Driver;
    using Microsoft.Extensions.Logging;
    using SeedWorks;
    using Settings;

    public sealed class MongoRepository<T> : IMongoRepository<T>
        where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly ILogger<MongoRepository<T>> _logger;
        private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

        public MongoRepository(ILogger<MongoRepository<T>> logger, IMongoDatabase mongoDatabase, string collectionName)
        {
            if (mongoDatabase == null) throw new ArgumentNullException(nameof(mongoDatabase));
            _logger = logger;

            _collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public ValueTask<List<T>> Get()
        {
            try
            {
                var result = _collection
                    .Find(_filterDefinitionBuilder.Empty)
                    .ToListAsync()
                    .FastResult();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                throw;
            }
        }

        public ValueTask<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _collection
                    .Find(filter)
                    .FirstOrDefaultAsync()
                    .FastResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                throw;
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            try
            {
                return await _collection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            try
            {
                var items = await _collection.Find(filter).ToListAsync();
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(T newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            try
            {
                await _collection.InsertOneAsync(newItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(IEnumerable<T> newItems)
        {
            if (newItems == null) throw new ArgumentNullException(nameof(newItems));

            var items = newItems.ToArray();
            var capacity = items.Length;
            if (capacity == 0) throw new ArgumentNullException(nameof(newItems));

            var tasks = new List<Task>(capacity);
            for (var index = 0; index < capacity; index++)
            {
                tasks.Add(Create(items[index]));
            }

            foreach (var task in tasks)
            {
                try
                {
                    if (!task.IsCompletedSuccessfully)
                        await task;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, item.Id);

            try
            {
                var replaceResult = await _collection.ReplaceOneAsync(filter, item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}