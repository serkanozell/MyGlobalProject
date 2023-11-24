using MyGlobalProject.Persistance.Context;

namespace MyGlobalProject.Tests.TestSetup
{
    public static class Categories
    {
        public static void AddCategories(this AppDbContext context)
        {
            context.Categories.AddRange(
                new Domain.Entities.Category
                {
                    Name = "Test-Accessory",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new Domain.Entities.Category
                {
                    Name = "Test-Car",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                }
                );
        }
    }
}
