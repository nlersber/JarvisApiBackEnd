using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain.Enums;

namespace JarvisNG.Models.Domain {
    public class Item {
        public double Price { get; }
        public string Name { get; set; }
        public ProductType Type { get; }

        public Item() {

        }
        public Item(double price, string name, ProductType type) {
            Price = price;
            Name = name;
            Type = type;
        }
    }
}
