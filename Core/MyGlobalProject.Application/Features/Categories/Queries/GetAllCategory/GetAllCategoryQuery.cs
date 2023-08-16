using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<List<CategoryListDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryListDTO>>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;

            public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
            }

            public async Task<List<CategoryListDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                var categoryList = _categoryReadRepository.GetAll().ToList();

                var mappedCategories = _mapper.Map<List<CategoryListDTO>>(categoryList);

                var resultCategoryList = _mapper.Map<List<CategoryListDTO>>(mappedCategories);

                return resultCategoryList;
            }
        }
    }
}
