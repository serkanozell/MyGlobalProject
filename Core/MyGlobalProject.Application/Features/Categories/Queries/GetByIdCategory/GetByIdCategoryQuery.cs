using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<GetByIdCategoryDTO>
    {
        public Guid Id { get; set; }
        public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, GetByIdCategoryDTO>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;

            public GetByIdCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdCategoryDTO> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                var mappedCategory = _mapper.Map<Category>(request);
                var category = await _categoryReadRepository.GetByIdAsync(mappedCategory.Id);
                var getCategoryDTO = _mapper.Map<GetByIdCategoryDTO>(category);
                return getCategoryDTO;
            }
        }
    }
}
