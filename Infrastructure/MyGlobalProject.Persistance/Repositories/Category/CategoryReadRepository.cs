using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
