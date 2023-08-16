using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Common;
using MyGlobalProject.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public IQueryable<T> GetAll() => Table.AsQueryable();

        #region getall için alternatif olarak kullanılabilir
        //public List<TEntity> GetAll(Expression<Func<TEntity, bool>> condition = null)
        //{
        //    return condition == null
        //        ? _context.Set<TEntity>().ToList()
        //        : _context.Set<TEntity>().Where(condition).ToList();
        //}
        #endregion

        public IQueryable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }
    }
}
