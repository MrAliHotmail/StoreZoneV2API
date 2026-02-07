using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Entities;

namespace StoreZoneV2API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    
        public DbSet<Product> Products => Set<Product>(); // same as : public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p=>p.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
                builder.Property(n => n.Price).IsRequired().HasPrecision(18, 2);
                builder.HasIndex(p => p.Name).IsUnique(false);


            });
            modelBuilder.Entity<Category>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
                builder.HasMany(c => c.Products)
                       .WithOne(p => p.Category)
                       .HasForeignKey(p => p.CategoryId)
                       .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);
                builder.Property(o => o.OrderDate).IsRequired();
                builder.Property(o => o.TotalAmount).IsRequired().HasPrecision(18, 2);
            });
            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(oi => oi.Id);
                builder.Property(oi => oi.Quantity).IsRequired();
                builder.Property(oi => oi.UnitPrice).IsRequired().HasPrecision(18, 2);
                builder.HasOne(oi => oi.Order)
                       .WithMany(o => o.OrderItems)
                       .HasForeignKey(oi => oi.OrderId)
                       .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(oi => oi.Product)
                       .WithMany()
                       .HasForeignKey(oi => oi.ProductId)
                       .OnDelete(DeleteBehavior.Restrict);
            });
           
        }
    }
}
