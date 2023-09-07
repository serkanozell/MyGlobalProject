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
            => expression == null ? Table.AsQueryable() : Table.Where(expression)
                                                               .AsQueryable();

        public IQueryable<T> GetBy(Expression<Func<T, bool>> expression,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            return Table.Where(expression);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }
    }
}
