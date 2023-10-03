using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyGlobalProject.Application.Features.Categories.Commands.CreateCategory;
using MyGlobalProject.Application.Features.Categories.Commands.DeleteCategory;
using MyGlobalProject.Application.Features.Categories.Commands.UpdateCategory;
using MyGlobalProject.Application.Features.Categories.Queries.GetByIdCategory;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.CreateOrderItemWithoutRegister;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Commands.DeleteOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Commands.UpdateOrderItem;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetAllOrderItemByOrderId;
using MyGlobalProject.Application.Features.OrderItems.Queries.GetByIdOrderItem;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrder;
using MyGlobalProject.Application.Features.Orders.Commands.CreateOrderWithoutRegister;
using MyGlobalProject.Application.Features.Orders.Commands.DeleteOrder;
using MyGlobalProject.Application.Features.Orders.Commands.UpdateOrder;
using MyGlobalProject.Application.Features.Orders.Queries.GetAllOrderByUserId;
using MyGlobalProject.Application.Features.Orders.Queries.GetByIdOrder;
using MyGlobalProject.Application.Features.Products.Commands.CreateProduct;
using MyGlobalProject.Application.Features.Products.Commands.DeleteProduct;
using MyGlobalProject.Application.Features.Products.Commands.UpdateProduct;
using MyGlobalProject.Application.Features.Products.Queries.GetAllProductByCategoryId;
using MyGlobalProject.Application.Features.Products.Queries.GetByIdProduct;
using MyGlobalProject.Application.Features.Roles.Commands.CreateRole;
using MyGlobalProject.Application.Features.Roles.Commands.DeleteRole;
using MyGlobalProject.Application.Features.Roles.Commands.UpdateRole;
using MyGlobalProject.Application.Features.Roles.Queries.GetByIdRole;
using MyGlobalProject.Application.Features.UserAddresses.Commands.CreateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.DeleteUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Commands.UpdateUserAddress;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetAllUserAddressByUserId;
using MyGlobalProject.Application.Features.UserAddresses.Queries.GetByIdUserAddress;
using MyGlobalProject.Application.Features.Users.Commands.CreateUser;
using MyGlobalProject.Application.Features.Users.Commands.DeleteUser;
using MyGlobalProject.Application.Features.Users.Commands.LoginUser;
using MyGlobalProject.Application.Features.Users.Commands.RegisterUser;
using MyGlobalProject.Application.Features.Users.Commands.UpdateUser;
using MyGlobalProject.Application.Features.Users.Queries.GetByIdUsers;
using MyGlobalProject.Application.ServiceInterfaces.Caching;
using MyGlobalProject.Application.ServiceInterfaces.JWT;
using MyGlobalProject.Application.ServiceInterfaces.Notification;
using MyGlobalProject.Infrastructure.Caching;
using MyGlobalProject.Infrastructure.Notification;
using System.Text;

namespace MyGlobalProject.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IValidator<GetAllProductByCategoryIdQuery>, GetAllProductByCategoryIdQueryValidator>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidator>();
            services.AddScoped<IValidator<GetByIdUserQuery>, GetByIdUserQueryValidator>();

            services.AddScoped<IValidator<CreateUserAddressCommand>, CreateUserAddressCommandValidator>();
            services.AddScoped<IValidator<UpdateUserAddressCommand>, UpdateUserAddressCommandValidator>();
            services.AddScoped<IValidator<DeleteUserAddressCommand>, DeleteUserAddressCommandValidator>();
            services.AddScoped<IValidator<GetByIdUserAddressQuery>, GetByIdUserAddressQueryValidator>();
            services.AddScoped<IValidator<GetAllUserAddressByUserIdQuery>, GetAllUserAddressByUserIdQueryValidator>();
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginUserCommandValidator>();

            services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            services.AddScoped<IValidator<CreateOrderWithoutRegisterCommand>, CreateOrderWithoutRegisterCommandValidator>();
            services.AddScoped<IValidator<UpdateOrderCommand>, UpdateOrderCommandValidator>();
            services.AddScoped<IValidator<DeleteOrderCommand>, DeleteOrderCommandValidator>();
            services.AddScoped<IValidator<GetByIdOrderQuery>, GetByIdOrderQueryValidator>();
            services.AddScoped<IValidator<GetAllOrderByUserIdQuery>, GetAllOrderByUserIdQueryValidator>();

            services.AddScoped<IValidator<CreateOrderItemCommand>, CreateOrderItemCommandValidator>();
            services.AddScoped<IValidator<CreateOrderItemWithoutRegisterCommand>, CreateOrderItemWithoutRegisterCommandValidator>();
            services.AddScoped<IValidator<UpdateOrderItemCommand>, UpdateOrderItemCommandValidator>();
            services.AddScoped<IValidator<DeleteOrderItemCommand>, DeleteOrderItemCommandValidator>();
            services.AddScoped<IValidator<DeleteOrderItemByOrderIdCommand>, DeleteOrderItemByOrderIdCommandValidator>();
            services.AddScoped<IValidator<GetByIdOrderItemQuery>, GetByIdOrderItemQueryValidator>();
            services.AddScoped<IValidator<GetAllOrderItemByOrderIdQuery>, GetAllOrderItemByOrderIdQueryValidator>();

            services.AddScoped<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>();
            services.AddScoped<IValidator<UpdateRoleCommand>, UpdateRoleCommandValidator>();
            services.AddScoped<IValidator<DeleteRoleCommand>, DeleteRoleCommandValidator>();
            services.AddScoped<IValidator<GetByIdRoleQuery>, GetByIdRoleQueryValidator>();

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ITokenHandler, Token.TokenHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, //oluşturulacak token değerini kimlerin kullanacağını belirlediğimiz değer => www.xxx.com
                    ValidateIssuer = true,   //oluşturulacak token değerini kimin dağıttığını ifade eden alan => www.xapi.com
                    ValidateLifetime = true, //oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                    ValidateIssuerSigningKey = true, //üretilecek token değerinin uygulamaya ait bir değer olup olmadığını ifade eden security key verisinin doğrulanmasıdır

                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!))
                };
            });
        }
    }
}
