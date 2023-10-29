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

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Now;

            await Table.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.CreatedDate = DateTime.Now;

                await Table.AddAsync(entity, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            entity.IsActive = false;
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;

            Table.Update(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<List<T>> DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = false;
                entity.IsDeleted = true;
                entity.UpdatedDate = DateTime.Now;

                Table.Update(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            entity.UpdatedDate = DateTime.Now;
            Table.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<List<T>> UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
                Table.Update(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }
    }
}
