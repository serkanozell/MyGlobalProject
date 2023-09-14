using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Application.RepositoryInterfaces
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<List<T>> DeleteRangeAsync(List<T> entities);
    }
}
