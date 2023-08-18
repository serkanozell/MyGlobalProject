using MyGlobalProject.Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Dto.CategoryDtos
{
    public class GetByIdCategoryDTO:BaseDTO
    {
        public string Name { get; set; }
    }
}
