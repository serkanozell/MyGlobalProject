using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    public class UserAddressWriteRepository : WriteRepository<UserAddress>, IUserAddressWriteRepository
    {
        public UserAddressWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
