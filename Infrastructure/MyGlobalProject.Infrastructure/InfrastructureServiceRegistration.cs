using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory;

namespace MyGlobalProject.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddScoped<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();
            services.AddScoped<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>();

            services.AddScoped<IValidator<GetByIdQuery>, GetByIdCategoryQueryValidator>();
        }
    }
}
