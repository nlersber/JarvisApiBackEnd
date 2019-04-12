﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JarvisNG.Data {
    public class DataContext : DbContext {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(s => s.id);

            modelBuilder.Entity<Item>().HasKey(s => s.Name);

            modelBuilder.Entity<User>().HasData(
                new User { id = 1, Name = "Test", Balance = 0, IsAdmin = true, Password = "Password" },
                new User { id = 2, Name = "Test2", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 3, Name = "Test3", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 3, Name = "Test4", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 4, Name = "Test5", Balance = 0, IsAdmin = false, Password = "Password" },
                new User { id = 5, Name = "Test6", Balance = 0, IsAdmin = false, Password = "Password" }
                );


        }
    }
}