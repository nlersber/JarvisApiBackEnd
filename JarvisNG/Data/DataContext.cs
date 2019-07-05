﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace JarvisNG.Data {
    public class DataContext : DbContext {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().Property(s => s.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasKey(s => s.id);

            modelBuilder.Entity<Item>().Property(s => s.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Item>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Item>().HasKey(s => s.Id);

            //modelBuilder.Entity<Order>().Property(s => s.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Order>().Property(s => s.Items).IsRequired();
            //modelBuilder.Entity<Order>().HasKey(s => s.Id);
            //modelBuilder.Entity<Order>().HasOne(s => s.User);

            //Dictionary<Item, int> dic = new Dictionary<Item, int>();
            //dic.Add(new Item { Id = 1, Name = "Cola", Price = 1.5, Category = ProductType.Beverage, Count = 50 }, 2);
            //dic.Add(new Item { Id = 2, Name = "Cola Zero", Price = 1.5, Category = ProductType.Beverage, Count = 50 }, 3);
            ////new Item { Id = 1, Name = "Cola", Price = 1.5, Category = ProductType.Beverage, Count = 50 },
            ////new Item { Id = 2, Name = "Cola Zero", Price = 1.5, Category = ProductType.Beverage, Count = 50 },

            //modelBuilder.Entity<Order>().HasData(new Order { User = User.DEFAULT, Id = 1, Items = dic });


            modelBuilder.Entity<User>().HasData(
                User.DEFAULT,
                new User { id = 2, Name = "Test", Balance = 0, IsAdmin = true, Password = "Password" },
                new User { id = 3, Name = "Test2", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 4, Name = "Test3", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 5, Name = "Test4", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 6, Name = "Test5", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 7, Name = "Test6", Balance = 0, IsAdmin = false, Password = "Password" }
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