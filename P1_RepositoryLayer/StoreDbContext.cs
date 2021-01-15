using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P1_ModelLib.Models;

namespace P1_RepositoryLayer
{
    public class StoreDbContext : IdentityDbContext< Customer , IdentityRole<int> ,int >
    {
        //The IdentityDbContext Already contains a User, so we are goint to have a StoreDBContext with the IdentityCustomer
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }

        public StoreDbContext() { }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //To seed some data...
            //modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "http://sample.com" });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-0IVLTFI;Database=P1_Test1;Trusted_Connection=True");

            }

        }
    }
}
