using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;

namespace JarvisNG.Models.IRepositories {
    public interface IUserRepository {
        User GetBy(string name);
        void AddUser(User user);
        void AddBalance(string name, double balance);
        void SubtractBalance(string name, double balance);
        void MakeAdmin(string name);
        void RemoveAdmin(string name);

        void SaveChanges();
    }
}
