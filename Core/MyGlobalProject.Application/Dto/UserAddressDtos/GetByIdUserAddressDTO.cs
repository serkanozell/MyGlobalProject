using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.UserAddressDtos
{
    public class GetByIdUserAddressDTO : BaseDTO
    {
        public string Address { get; set; }
        public string AddressTitle { get; set; }
    }
}
