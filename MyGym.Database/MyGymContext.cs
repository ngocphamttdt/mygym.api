using Microsoft.EntityFrameworkCore;
using MyGym.Database.Entities;
using MyGym.Database.Mapping;

namespace MyGym.Database
{
    public class MyGymContext : DbContext
    {
        public MyGymContext(DbContextOptions<MyGymContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
