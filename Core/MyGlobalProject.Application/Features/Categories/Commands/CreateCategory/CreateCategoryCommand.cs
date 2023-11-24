using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using Serilog;
using static System.Net.WebUtility;

namespace MyGlobalProject.Application.Features.Categories.Commands.CreateCategory
{

    public class CreateCategoryCommand : IRequest<GenericResponse<CreateCategoryDTO>>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GenericResponse<CreateCategoryDTO>>
        {

            private readonly ICategoryReadRepository _categoryReadRepository;
            private readonly ICategoryWriteRepository _categoryWriteRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;


            public CreateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository,
                                                IMapper mapper,
                                                ICategoryWriteRepository categoryWriteRepository,
                                                ICacheService cacheService)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<GenericResponse<CreateCategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var mappedCategory = _mapper.Map<Category>(request);
                    mappedCategory.Name = HtmlEncode(mappedCategory.Name);

                    var createdCategory = await _categoryWriteRepository.AddAsync(mappedCategory, cancellationToken);
                    var createdCategoryDTO = _mapper.Map<CreateCategoryDTO>(createdCategory);

                    GenericResponse<CreateCategoryDTO> response = new GenericResponse<CreateCategoryDTO>()
                    {
                        Data = createdCategoryDTO,
                        Success = true,
                        Message = "Category added successfully"
                    };

                    Log.Information($"Category Added. Id = {createdCategory.Id}, Name = {createdCategory.Name}, Date = {createdCategory.CreatedDate}");

                    await _cacheService.RemoveAllKeysAsync();

                    return response;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
