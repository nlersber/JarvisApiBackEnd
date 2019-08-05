using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;

namespace JarvisNG.Models.IRepositories {
    public interface IUserRepository {
        User GetBy(string name);
        void AddUser(User user);
        void AddBalance(int id, double balance);
        void SubtractBalance(int id, double balance);
        void MakeAdmin(string name);
        bool IsAdmin(string name);
        void RemoveAdmin(string name);
        IEnumerable<ManagementUserDTO> GetUsers();

        User GetDefault();
        void SaveChanges();
    }
}
