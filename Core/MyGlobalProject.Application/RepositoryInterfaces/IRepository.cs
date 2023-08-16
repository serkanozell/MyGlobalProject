using MyGlobalProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        //Task<T?> GetByAsync(Expression<Func<T, bool>> expression);
        //Task<List<T>> GetAllAsync();
        //Task<T?> GetByIdAsync(Guid id);
        //Task<T> AddAsync(T entity);
        //Task<T> UpdateAsync(T entity);
        //Task<T> DeleteAsync(T entity);
    }
}
