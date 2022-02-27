namespace Play.Catalog.Service.Core.Infra.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.SeedWorkds;

    public interface IMongoRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> Get();

        Task<T> GetById(string id);

        Task Create(T newEntity);

        Task Create(IEnumerable<T> newItems);

        Task Update(T item);
    }
}