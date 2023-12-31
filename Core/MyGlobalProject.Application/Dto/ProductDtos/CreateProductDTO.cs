﻿using MyGlobalProject.Application.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Application.Dto.ProductDtos
{
    public class CreateProductDTO:BaseAddDTO
    {
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
