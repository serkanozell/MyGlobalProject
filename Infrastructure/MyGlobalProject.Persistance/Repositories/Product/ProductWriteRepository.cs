using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
