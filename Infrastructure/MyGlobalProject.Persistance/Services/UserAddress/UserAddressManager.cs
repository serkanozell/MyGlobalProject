using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Dto.UserAddressDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.UserAddressServices;

namespace MyGlobalProject.Persistance.Services.UserAddress
{
    public class UserAddressManager : IUserAddressService
    {
        private readonly IUserAddressReadRepository _userAddressReadRepository;
        private readonly IMapper _mapper;

        public UserAddressManager(IUserAddressReadRepository userAddressReadRepository, IMapper mapper)
        {
            _userAddressReadRepository = userAddressReadRepository;
            _mapper = mapper;
        }

        public async Task<List<UserAddressListDTO>> GetAllUserAddress()
        {
            return _mapper.Map<List<UserAddressListDTO>>(await _userAddressReadRepository.GetBy(ua => ua.IsActive && !ua.IsDeleted).ToListAsync());
        }

        public async Task<List<UserAddressListDTO>> GetAllUserAddressByUserId(Guid userId)
        {
            return _mapper.Map<List<UserAddressListDTO>>(await _userAddressReadRepository.GetBy(ua => ua.UserId == userId && ua.IsActive && !ua.IsDeleted).ToListAsync());
        }

        public async Task<GetByIdUserAddressDTO> GetById(Guid addressId)
        {
            return _mapper.Map<GetByIdUserAddressDTO>(await _userAddressReadRepository.GetByIdAsync(addressId));
        }
    }
}
