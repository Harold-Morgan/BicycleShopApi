using BicycleService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LeasingEntry> LeasingEntries { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShopClient> ShopClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bicycle>().ToTable("Bicycle");
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Discount>().ToTable("Discount");
            modelBuilder.Entity<LeasingEntry>().ToTable("Leasing");

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<ShopClient>().ToTable("ShopClient");
        }
    }
}
