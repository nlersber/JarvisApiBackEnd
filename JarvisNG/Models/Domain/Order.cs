using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class Order {
        public Dictionary<Item, int> Items { get; set; }
        public IEnumerable<Item> ItemsList { get; }
        public User User { get; set; }
        public int Id { get; set; }

        public Order() {

        }

        public Order(Dictionary<Item, int> Items, User User) {
            this.Items = Items;
            this.User = User;
            flatten(Items);
        }

        private flatten(Dictionary<Item, int> Items) {
            ItemsList = new IEnumerable<Item>();
            foreach (KeyValuePair<Item, int> entry in ItemsList) {
                for (int i = 0; i < entry.Value; i++) 
                    ItemsList.Add(entry.Key);
            }
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
