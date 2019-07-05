using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;
using JarvisNG.Models.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace JarvisNG.Data.Repositories {
    public class ItemRepository : IItemRepository {

        private readonly DataContext context;
        private readonly DbSet<Item> items;

        public ItemRepository(DataContext context) {
            this.context = context;
            items = context.Items;
        }

        public IEnumerable<Item> GetAll() {
            return items;
        }

        public Item GetByName(string name) {
            return items.FirstOrDefault(t => t.Name.Equals(name));
        }

        public Item GetById(int id) {
            return items.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Item> GetByProductType(ProductType type) {
            return items.Where(t => t.Category == type).AsEnumerable();
        }


        public void AddItem(Item item) {
            items.Add(item);
        }

        public void RemoveItem(string name) {
            items.Remove(items.Single(s => s.Name.Equals(name)));
        }

        public void AddCountToStock(int id, int amount) {
            items.Single(s => s.Id == id).AddAmount(amount);
        }

        public void SubtractCountFromStock(int id, int amount) {
            items.Single(s => s.Id == id).SubtractAmount(amount);
        }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
