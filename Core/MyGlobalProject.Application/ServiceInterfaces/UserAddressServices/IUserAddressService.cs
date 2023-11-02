using MyGlobalProject.Application.Dto.UserAddressDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.UserAddressServices
{
    public interface IUserAddressService
    {
        Task<List<UserAddressListDTO>> GetAllUserAddress();
        Task<List<UserAddressListDTO>> GetAllUserAddressByUserId(Guid userId);
        Task<GetByIdUserAddressDTO> GetById(Guid addressId);
    }
}
