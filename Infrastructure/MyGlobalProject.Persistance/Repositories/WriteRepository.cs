using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Common;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Now;

            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.CreatedDate = DateTime.Now;

                await Table.AddAsync(entity);
            }
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            entity.IsActive = false;
            entity.IsDeleted = true;

            Table.Update(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> DeleteRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.IsDeleted = true;

                Table.Update(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
