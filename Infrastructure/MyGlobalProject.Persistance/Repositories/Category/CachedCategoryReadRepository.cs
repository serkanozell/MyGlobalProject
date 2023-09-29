using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Distributed;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace MyGlobalProject.Persistance.Repositories
{
    public class CachedCategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        private readonly ICategoryReadRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly AppDbContext _context;

        public CachedCategoryReadRepository(ICategoryReadRepository decorated, AppDbContext context, IDistributedCache distributedCache) : base(context)
        {
            _decorated = decorated;
            _context = context;
            _distributedCache = distributedCache;
        }

        public IQueryable<Category> GetAll(Expression<Func<Category, bool>>? expression = null)
        {
            return _decorated.GetAll(expression);
        }

        public IQueryable<Category> GetBy(Expression<Func<Category, bool>> expression, Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null)
        {
            return _decorated.GetBy(expression, include);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            string key = $"category-{id}";

            string? cachedCategory = await _distributedCache.GetStringAsync(key);

            Category? category;

            if (string.IsNullOrEmpty(cachedCategory))
            {
                category = await _decorated.GetByIdAsync(id);

                if (category is null)
                    return category;

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(category), options: new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) }); ;

                return category;
            }

            category = JsonConvert.DeserializeObject<Category>(cachedCategory);

            if (category is not null)
                _context.Set<Category>().Attach(category);

            return category;
        }

        public async Task<Category> GetSingleAsync(Expression<Func<Category, bool>> expression)
        {
            return await _decorated.GetSingleAsync(expression);
        }
    }
}
