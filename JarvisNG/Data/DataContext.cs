using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JarvisNG.Data {
    public class DataContext : IdentityDbContext {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> ShopUsers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DataContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<User>().Property(s => s.id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<User>().HasKey(s => s.id);
            modelBuilder.Entity<User>().Property(s => s.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(s => s.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(s => s.Balance).IsRequired();


            modelBuilder.Entity<Item>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Item>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Item>().HasKey(s => s.Id);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().HasKey(s => s.Id);
            //modelBuilder.Entity<Order>().Property(s => s.User.id);
            modelBuilder.Entity<Order>().HasOne(s => s.User).WithMany();

            modelBuilder.Entity<OrderItemWrapper>().HasKey(s => new { s.OrderId, s.ItemId });
            //modelBuilder.Entity<OrderItemWrapper>()


            modelBuilder.Entity<User>().HasData(
                User.DEFAULT,
                new User { id = 2, Name = "Test", Balance = 0, IsAdmin = true, Email = "Test1@Test.test" },
                new User { id = 3, Name = "Test2", Balance = 0, IsAdmin = false, Email = "Test2@Test.test" },
                new User { id = 4, Name = "Test3", Balance = 0, IsAdmin = false, Email = "Test3@Test.test" },
                new User { id = 5, Name = "Test4", Balance = 0, IsAdmin = false, Email = "Test4@Test.test" },
                new User { id = 6, Name = "Test5", Balance = 0, IsAdmin = false, Email = "Test5@Test.test" },
                new User { id = 7, Name = "Test6", Balance = 0, IsAdmin = false, Email = "Test6@Test.test" }
            );



            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Cola", Price = 1.5, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 2, Name = "Cola Zero", Price = 1.5, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 3, Name = "Fanta", Price = 1.5, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 4, Name = "Sprite", Price = 1.5, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 5, Name = "Red Bull", Price = 2F, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 6, Name = "Red Bull Zero", Price = 2F, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 7, Name = "Water", Price = 1F, Category = ProductType.Beverage, Count = 50 },
                new Item { Id = 8, Name = "Pizza", Price = 2.5, Category = ProductType.Food, Count = 50 },
                new Item { Id = 9, Name = "Hot Dog", Price = 1.5, Category = ProductType.Food, Count = 50 }
            );

            



        }
    }
}