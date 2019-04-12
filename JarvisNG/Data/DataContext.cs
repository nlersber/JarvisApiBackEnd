using System;
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
        }
    }
}
