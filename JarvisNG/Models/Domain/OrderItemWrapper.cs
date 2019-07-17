﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class OrderItemWrapper {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }

        public OrderItemWrapper() { }

        public OrderItemWrapper(Item item, int amount) {
            Item = item;
            Amount = amount;
        }


    }
}
