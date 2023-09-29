using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<GenericResponse<DeleteCategoryDTO>>
    {
        public Guid Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, GenericResponse<DeleteCategoryDTO>>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public DeleteCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, ICategoryWriteRepository categoryWriteRepository, ICacheService cacheService)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<DeleteCategoryDTO>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                GenericResponse<DeleteCategoryDTO> response = new();

                var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

                if (currentCategory == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Deleting error";

                    return response;
                }

                await _categoryWriteRepository.DeleteAsync(currentCategory);

                var deletedCategoryDTO = _mapper.Map<DeleteCategoryDTO>(currentCategory);

                response.Data = deletedCategoryDTO;
                response.Success = true;
                response.Message = "Category deleted successfully";

                Log.Information($"Category deleted. Id = {deletedCategoryDTO.Id}");

                await _cacheService.RemoveAllKeysAsync();

                return response;
            }
        }
    }
}
