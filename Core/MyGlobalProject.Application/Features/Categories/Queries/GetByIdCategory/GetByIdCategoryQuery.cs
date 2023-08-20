using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<GenericResponse<GetByIdCategoryDTO>>
    {
        public Guid Id { get; set; }
        public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, GenericResponse<GetByIdCategoryDTO>>
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

            public async Task<GenericResponse<GetByIdCategoryDTO>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<GetByIdCategoryDTO>();
                var mappedCategory = _mapper.Map<Category>(request);
                var category = await _categoryReadRepository.GetByIdAsync(mappedCategory.Id);
                if (category == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no category";

                    return response;
                }

                var getCategoryDTO = _mapper.Map<GetByIdCategoryDTO>(category);

                response.Data = getCategoryDTO;
                response.Message = "Success";

                return response;
            }
        }
    }
}
