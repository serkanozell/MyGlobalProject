using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Common;
using MyGlobalProject.Persistance.Context;
using System.Linq.Expressions;

namespace MyGlobalProject.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>()
                          .AddAsync(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                                     .FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                 .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
