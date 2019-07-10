using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class Order {
        public Dictionary<Item, int> Items { get; set; }
        public IList<Item> ItemsList { get; set; }
        public User User { get; set; }
        public int Id { get; set; }

        public Order() {

        }

        public Order(Dictionary<Item, int> Items, User User) {
            this.Items = Items;
            this.User = User;
            flatten(Items);
        }

        private void flatten(Dictionary<Item, int> Items) {
            ItemsList = new List<Item>();
            foreach (var item in Items) 
                for (int i = 0; i < item.Value; i++) 
                    ItemsList.Add(item.Key);
        }

        public bool CheckAvailability() {
            return Items.All(s => s.Key.CheckAvailability(s.Value));
        }

        public double GetTotal() {
            double total = 0;
            foreach (var entry in Items)
                total += (entry.Key.Price * entry.Value);
            return total;

        }



        public IEnumerable<Item> GetUnavailables() {
            return Items.Where(s => !s.Key.CheckAvailability(s.Value)).Select(s => s.Key);
        }
    }


}
