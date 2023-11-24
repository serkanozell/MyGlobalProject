using AutoMapper;
using FluentAssertions;
using MyGlobalProject.Application.Dto.CategoryDtos;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Mapping;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.Wrappers;
using MyGlobalProject.Domain.Entities;
using MyGlobalProject.Persistance.Context;
using MyGlobalProject.Tests.TestSetup;
using NSubstitute;
using static MyGlobalProject.Application.Features.Categories.Commands.CreateCategory.CreateCategoryCommand;

namespace MyGlobalProject.Tests.Application.Categories.Commands
{
    public class CreateCategoryCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryReadRepository _readRepository;
        private readonly ICategoryWriteRepository _writeRepository;
        private readonly ICacheService _cacheService;

        public CreateCategoryCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
            _readRepository = Substitute.For<ICategoryReadRepository>();
            _writeRepository = Substitute.For<ICategoryWriteRepository>();
            _cacheService = Substitute.For<ICacheService>();
        }

        [Fact]
        public async void Handle_Should_ReturnSuccessResult_WhenCategoryIsValid()
        {
            //Arrange
            var createdCategory = new Category
            {
                Name = "Test",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            var createCategoryCommand = new CreateCategoryCommand { Name = "Test" };

            _context.Categories.Add(createdCategory);
            _context.SaveChanges();

            CreateCategoryCommandHandler command = new CreateCategoryCommandHandler(_readRepository, _mapper, _writeRepository, _cacheService);

            var response = new GenericResponse<CreateCategoryDTO>();
            var createCategoryDto = new CreateCategoryDTO() { Name = "Test" };
            response.Data = createCategoryDto;

            var a = _writeRepository.AddAsync(createdCategory, new CancellationToken()).Returns(createdCategory);
            //command.Handle(createCategoryCommand, new CancellationToken()).Returns(response);

            //Act
            await FluentActions.Invoking(() => command.Handle(createCategoryCommand, new CancellationToken())).Invoke();

            //Assert
            var resultCategory = _context.Categories.SingleOrDefault(c => c.Name == createCategoryCommand.Name);

            resultCategory.Should().NotBeNull();
            resultCategory.Name.Should().Be(createCategoryCommand.Name);


        }
    }
}
