namespace MyGlobalProject.Application.Dto.UserAddressDtos
{
    public class CreateUserAddresDTO
    {
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
    }
}
