﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyGlobalProject.Domain.Entities;

namespace MyGlobalProject.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        //protected IConfiguration Configuration { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
