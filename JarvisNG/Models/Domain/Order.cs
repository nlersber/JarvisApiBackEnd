using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class Order {
        public Dictionary<Item, int> Items { get; }
        public User User { get; }
    }
}
