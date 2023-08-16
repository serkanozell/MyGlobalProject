using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;

namespace MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        //public DateTime CreateDate { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryDTO>
        {
            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;

            public UpdateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
            }

            public async Task<UpdateCategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

                currentCategory!.Name = request.Name;
                currentCategory.IsActive = request.IsActive;
                currentCategory.IsDeleted = request.IsDeleted;

                await _categoryWriteRepository.UpdateAsync(currentCategory);

                var updatedCategoryDTO = _mapper.Map<UpdateCategoryDTO>(currentCategory);


                //automapper ile createdate hatası oluyor
                //var mappedCategory = _mapper.Map<Category>(request);
                //var updatedCategory = await _categoryRepository.Update(mappedCategory);
                //var updatedCategoryDTO = _mapper.Map<UpdateCategoryDTO>(updatedCategory);

                return updatedCategoryDTO;
            }
        }
    }
}
