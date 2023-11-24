using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyGlobalProject.Application.Features.Categories.Mapping;
using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Tests.TestSetup
{
    public class CommonTestFixture
    {
        public AppDbContext Context { get; set; }
        public IMapper Mapper { get; set; }


        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("MyGlobalProjectTestDb").Options;
            Context = new AppDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddCategories();
            
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        }
    }
}
