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
            users = context.Users;
        }

        public User GetBy(string name) {
            return users.FirstOrDefault(t => t.Name.Equals(name));
        }

        public void AddUser(User user) {
            users.Add(user);
        }

        public void AddBalance(string name, double balance) {
            users.Single(s => s.Name.Equals(name)).AddBalance(balance);
        }

        public void SubtractBalance(string name, double balance) {
            users.Single(s => s.Name.Equals(name)).SubtractBalance(balance);
        }

        public void MakeAdmin(string name) {
            users.Single(s => s.Name.Equals(name)).IsAdmin = true;
        }

        public void RemoveAdmin(string name) {
            users.Single(s => s.Name.Equals(name)).IsAdmin = false;
        }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
