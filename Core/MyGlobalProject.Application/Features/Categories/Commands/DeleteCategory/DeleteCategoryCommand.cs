using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryDTO>
    {
        public Guid Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryDTO>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;

            public DeleteCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, ICategoryWriteRepository categoryWriteRepository)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
            }

            public async Task<DeleteCategoryDTO> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

                currentCategory!.IsActive = true;
                currentCategory.IsDeleted = false;

                await _categoryWriteRepository.UpdateAsync(currentCategory);

                var deletedCategoryDTO = _mapper.Map<DeleteCategoryDTO>(currentCategory);

                return deletedCategoryDTO;
            }
        }
    }
}
