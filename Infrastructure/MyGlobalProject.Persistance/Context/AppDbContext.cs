using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyGlobalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGlobalProject.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
    }
}
