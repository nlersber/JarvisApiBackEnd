using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;

namespace JarvisNG.Models.IRepositories {
    public interface IItemRepository {
        IEnumerable<Item> GetAll();
        Item GetByName(string name);
        IEnumerable<Item> GetByProductType(ProductType type);
        void AddItem(Item item);
        void RemoveItem(string name);
        void AddCountToStock(string name, int amount);
        void SubtractCountFromStock(string name, int amount);
        void SaveChanges();
    }
}
