using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class Order {
        public Dictionary<Item, int> Items { get; set; }
        public User User { get; set; }

        public bool checkAvailability() {
            return Items.All(s => s.Key.CheckAvailability(s.Value));
        }

        public IEnumerable<Item> GetUnavailables() {
            return Items.Where(s => !s.Key.CheckAvailability(s.Value)).Select(s => s.Key);
        }
    }
}
