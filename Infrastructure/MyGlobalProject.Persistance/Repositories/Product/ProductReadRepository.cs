using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
