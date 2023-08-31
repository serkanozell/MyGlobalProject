using MyGlobalProject.Application.Dto.Common;
using MyGlobalProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Dto.OrderDtos
{
    public class UpdateOrderDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string PhoneNumber { get; set; }
    }
}
