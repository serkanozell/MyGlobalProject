using MyGlobalProject.Domain.Common;
using System.Linq.Expressions;

namespace MyGlobalProject.Application.RepositoryInterfaces
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
    }
}
