using JarvisNG.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.IRepositories {
    public interface IOrderRepository {
        Task<IList<Order>> GetByUserId(int id);
        void Add(Order order);
        void SaveChanges();
    }
}
