using MyGlobalProject.Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Dto.CategoryDtos
{
    public class UpdateCategoryDTO : BaseUpdateDTO
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
