﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory;
using MyGlobalProject.Application.Features.Products.Commands.CreateProduct;
using MyGlobalProject.Application.Features.Products.Commands.DeleteProduct;
using MyGlobalProject.Application.Features.Products.Commands.UpdateProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct;
using MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Application.Features.Users.Commands.DeleteUser;
using MyGlobalProject.Application.Features.Users.Commands.UpdateUser;
using MyGlobalProject.Application.Features.Users.Queries.GetByIdUsers;

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
            services.AddScoped<IValidator<GetByIdCategoryQuery>, GetByIdCategoryQueryValidator>();

            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidatior>();
            services.AddScoped<IValidator<GetByIdProductQuery>, GetByIdProductQueryValidator>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidator>();
            services.AddScoped<IValidator<GetByIdUserQuery>, GetByIdUserQueryValidator>();

            services.AddScoped<IValidator<CreateUserAddressCommand>, CreateUserAddressCommandValidator>();

            services.AddScoped<IValidator<DeleteUserAddressCommand>, DeleteUserAddressCommandValidator>();

        }
    }
}
