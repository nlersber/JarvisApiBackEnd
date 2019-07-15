using JarvisNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.IRepositories {
    interface IOrderRepository {
        IEnumerable<Order> GetByUserId(int id);
        Order GetDetailById(int id);
        void SaveChanges();
    }
}
