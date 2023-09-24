using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using Serilog;

namespace MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<GenericResponse<UpdateCategoryDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public DateTime CreateDate { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GenericResponse<UpdateCategoryDTO>>
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

            public async Task<GenericResponse<UpdateCategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                GenericResponse<UpdateCategoryDTO> response = new();

                var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

                if (currentCategory == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "There is no category to update";

                    return response;
                }

                currentCategory!.Name = request.Name;

                await _categoryWriteRepository.UpdateAsync(currentCategory);

                var updatedCategoryDTO = _mapper.Map<UpdateCategoryDTO>(currentCategory);

                response.Data = updatedCategoryDTO;
                response.Message = "Category updated successfully";

                //automapper ile createdate hatası oluyor
                //var mappedCategory = _mapper.Map<Category>(request);
                //var updatedCategory = await _categoryRepository.Update(mappedCategory);
                //var updatedCategoryDTO = _mapper.Map<UpdateCategoryDTO>(updatedCategory);

                Log.Information($"Category updated. Old name = {currentCategory.Name}. New name = {updatedCategoryDTO.Name}");

                return response;
            }
        }
    }
}
