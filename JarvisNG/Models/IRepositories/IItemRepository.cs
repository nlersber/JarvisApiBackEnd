using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;

namespace JarvisNG.Models.IRepositories {
    interface IItemRepository {
        Item GetByName(string name);
        IEnumerable<Item> GetByProductType(ProductType type);
        void SaveChanges();
    }
}
