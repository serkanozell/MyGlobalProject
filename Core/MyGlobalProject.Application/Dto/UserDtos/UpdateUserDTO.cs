using MyGlobalProject.Application.Dto.Common;

namespace MyGlobalProject.Application.Dto.UserDtos
{
    public class UpdateUserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
