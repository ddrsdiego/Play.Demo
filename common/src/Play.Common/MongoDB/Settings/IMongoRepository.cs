namespace Play.Common.MongoDB.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using SeedWorks;

    public interface IMongoRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> Get();

        Task<IReadOnlyCollection<T>> Get(Expression<Func<T, bool>> filter);

        Task<T> GetById(string id);

        Task Create(T newEntity);

        Task Create(IEnumerable<T> newItems);

        Task Update(T item);
    }
}