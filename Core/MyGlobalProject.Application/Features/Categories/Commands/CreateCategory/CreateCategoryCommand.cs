﻿using AutoMapper;
using MediatR;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;

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


            public CreateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, ICategoryWriteRepository categoryWriteRepository)
            {
                _categoryReadRepository = categoryReadRepository;
                _categoryWriteRepository = categoryWriteRepository;
                _mapper = mapper;
            }

            public async Task<GenericResponse<CreateCategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var mappedCategory = _mapper.Map<Category>(request);

                var createdCategory = await _categoryWriteRepository.AddAsync(mappedCategory);
                var createdCategoryDTO = _mapper.Map<CreateCategoryDTO>(createdCategory);

                GenericResponse<CreateCategoryDTO> response = new GenericResponse<CreateCategoryDTO>()
                {
                    Data = createdCategoryDTO,
                    Success = true,
                    Message = "Category added successfully"
                };

                return response;
            }
        }
    }
}
