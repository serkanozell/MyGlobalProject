using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Common;
using MyGlobalProject.Persistance.Context;
using System.Linq.Expressions;

namespace MyGlobalProject.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null)
            => expression == null ? GetQueryableAllActive() : GetQueryableAllActive().Where(expression)
                                                               .AsQueryable();

        public async IQueryable<T> GetBy(Expression<Func<T, bool>> expression,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            return GetQueryableAllActive().Where(expression);
        }

        public IQueryable<T> GetQueryable()
        {
            return Table.Where(x => x.IsDeleted != true);
        }

        public IQueryable<T> GetQueryableAllActive()
        {
            return GetQueryable().Where(x => x.IsActive);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await GetQueryableAllActive().FirstOrDefaultAsync(x => x.Id == id) ?? new T();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression) ?? new T();
        }
    }
}
