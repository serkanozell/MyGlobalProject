using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGlobalProject.Application.RepositoryInterfaces;
using MyGlobalProject.Application.ServiceInterfaces.CategoryServices;
using MyGlobalProject.Application.ServiceInterfaces.OrderItemsServices;
using MyGlobalProject.Application.ServiceInterfaces.OrdersServices;
using MyGlobalProject.Application.ServiceInterfaces.ProductServices;
using MyGlobalProject.Application.ServiceInterfaces.RolesServices;
using MyGlobalProject.Application.ServiceInterfaces.UserAddressServices;
using MyGlobalProject.Application.ServiceInterfaces.UserServices;
using MyGlobalProject.Persistance.Context;
using MyGlobalProject.Persistance.Repositories;
using MyGlobalProject.Persistance.Services.Category;
using MyGlobalProject.Persistance.Services.Order;
using MyGlobalProject.Persistance.Services.OrderItem;
using MyGlobalProject.Persistance.Services.Product;
using MyGlobalProject.Persistance.Services.Role;
using MyGlobalProject.Persistance.Services.User;
using MyGlobalProject.Persistance.Services.UserAddress;

namespace MyGlobalProject.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConn"));
            });

            services.AddHangfire(config =>
            {
                //var option = new SqlServerStorageOptions
                //{
                //    PrepareSchemaIfNecessary = true,
                //    QueuePollInterval = TimeSpan.FromMinutes(5),
                //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                //    UseRecommendedIsolationLevel = true,
                //    DisableGlobalLocks = true,
                //};
                //config.UseSqlServerStorage(configuration.GetConnectionString("HanfireDb"), option)
                //      .WithJobExpirationTimeout(TimeSpan.FromHours(1));    
                config.UseSqlServerStorage(configuration.GetConnectionString("HanfireDb"));
            });

            services.AddHangfireServer();

            //services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.Decorate<ICategoryReadRepository, CachedCategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
            services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddScoped<IUserAddressReadRepository, UserAddressReadRepository>();
            services.AddScoped<IUserAddressWriteRepository, UserAddressWriteRepository>();

            services.AddScoped<IRoleReadRepository, RoleReadRepository>();
            services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderItemService, OrderItemManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserAddressService, UserAddressManager>();
            services.AddScoped<IRoleService, RoleManager>();


            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = configuration.GetConnectionString("Redis")!;

                redisOptions.Configuration = connection;
            });

            return services;
        }
    }
}
