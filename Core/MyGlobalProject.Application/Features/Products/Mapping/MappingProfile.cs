using AutoMapper;
using MyGlobalProject.Application.Dto.ProductDtos;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Products.Commands.CreateProduct;
using MyGlobalProject.Application.Features.Products.Commands.UpdateProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetAllProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Products.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();

            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();

            CreateMap<Product, DeleteProductDTO>().ReverseMap();
            CreateMap<Product, DeleteCategoryCommand>().ReverseMap();

            CreateMap<Product, GetByIdProductDTO>().ReverseMap();
            CreateMap<Product, GetByIdProductQuery>().ReverseMap();

            CreateMap<Product, ProductListDTO>().ReverseMap();
            CreateMap<Product, GetAllProductQuery>().ReverseMap();
        }
    }
}
