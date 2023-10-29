using MyGlobalProject.Domain.Common;

namespace MyGlobalProject.Application.RepositoryInterfaces
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<List<T>> UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<List<T>> DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken);
    }
}
