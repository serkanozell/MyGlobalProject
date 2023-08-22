using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Persistance.Repositories
{
    public class UserAddressReadRepository : ReadRepository<UserAddress>, IUserAddressReadRepository
    {
        public UserAddressReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
