using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;

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
            private readonly ICacheService _cacheService;

            public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper, ICacheService cacheService)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<List<CategoryListDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                List<CategoryListDTO>? categoryListDTOs = await _cacheService.GetAsync<List<CategoryListDTO>>("categories");

                if (categoryListDTOs is not null)
                    return categoryListDTOs;


                var categoryList = _categoryReadRepository.GetAll().ToList();

                var mappedCategories = _mapper.Map<List<CategoryListDTO>>(categoryList);

                var resultCategoryList = _mapper.Map<List<CategoryListDTO>>(mappedCategories);

                await _cacheService.SetAsync("categories", resultCategoryList);

                return resultCategoryList;
            }
        }
    }
}
