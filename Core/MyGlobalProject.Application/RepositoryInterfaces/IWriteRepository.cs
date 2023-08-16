using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Application.RepositoryInterfaces
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
