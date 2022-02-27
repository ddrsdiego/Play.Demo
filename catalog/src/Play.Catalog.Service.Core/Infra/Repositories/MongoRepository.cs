namespace Play.Catalog.Service.Core.Infra.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.SeedWorkds;
    using MongoDB.Driver;

    public sealed class MongoRepository<T> : IMongoRepository<T>
        where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            if (mongoDatabase == null) throw new ArgumentNullException(nameof(mongoDatabase));

            _collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> Get()
        {
            var items = await _collection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
            return items;
        }

        public async Task<T> GetById(string id)
        {
            try
            {
                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);

                var item = await _collection.Find(filter).FirstOrDefaultAsync();
                return item ?? default;
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
                await _collection.ReplaceOneAsync(filter, item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}