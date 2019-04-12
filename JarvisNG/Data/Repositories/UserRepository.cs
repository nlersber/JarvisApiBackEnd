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

        public User GetBy(string name) {
            return users.FirstOrDefault(t => t.Name.Equals(name));
        }
    }
}
