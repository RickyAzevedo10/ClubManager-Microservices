using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        IQueryable<T> GetEntity();
        void Update(T entity);
        public Task<T> GetById(long id);
        public Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAllAsync();
        Task<T> UpdateAsync(T entity);
    }
}