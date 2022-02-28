namespace Play.Common.MongoDB.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using SeedWorks;

    public interface IMongoRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAll();

        Task<IReadOnlyCollection<T>> GetAll(Expression<Func<T, bool>> filter);

        ValueTask<List<T>> Get();
        
        ValueTask<T> Get(Expression<Func<T, bool>> filter);

        Task Create(T newEntity);

        Task Create(IEnumerable<T> newItems);

        Task Update(T item);
    }
}