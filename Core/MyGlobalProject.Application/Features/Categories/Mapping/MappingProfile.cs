using AutoMapper;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetAllCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Categories.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();

            CreateMap<Category, GetByIdCategoryDTO>().ReverseMap();
            CreateMap<Category, GetByIdCategoryQuery>().ReverseMap();

            CreateMap<Category, CategoryListDTO>().ForMember(c => c.Id, opt => opt.MapFrom(dest => dest.Id))
                                                  .ForMember(x => x.Name, opts => opts.MapFrom(dests => dests.Name))
                                                  .ReverseMap();
            CreateMap<CategoryListDTO, GetAllCategoryQuery>().ReverseMap();

            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();

            CreateMap<Category, DeleteCategoryDTO>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
        }
    }
}
