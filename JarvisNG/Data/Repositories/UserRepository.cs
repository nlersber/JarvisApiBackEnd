using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace JarvisNG.Data.Repositories {
    public class UserRepository : IUserRepository {

        private readonly DataContext context;
        private readonly DbSet<User> users;

        public UserRepository(DataContext context) {
            this.context = context;
            users = context.ShopUsers;
        }

        public User GetBy(string name) {
            return users.FirstOrDefault(t => t.Name.Equals(name));
        }

        public void AddUser(User user) {
            users.Add(user);
        }

        public void AddBalance(int id, double balance) {
            users.Single(s => s.id == id).AddBalance(balance);
        }

        public void SubtractBalance(int id, double balance) {
            users.Single(s => s.id == id).SubtractBalance(balance);
        }

        public void MakeAdmin(string name) {
            users.Single(s => s.Name.Equals(name)).IsAdmin = true;
        }

        public void RemoveAdmin(string name) {
            users.Single(s => s.Name.Equals(name)).IsAdmin = false;
        }

        public User GetDefault() {
            return context.ShopUsers.FirstOrDefault(s => s.Name == "Default");
        }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
