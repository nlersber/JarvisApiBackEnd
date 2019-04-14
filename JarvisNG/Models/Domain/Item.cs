﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain.Enums;

namespace JarvisNG.Models.Domain {
    public class Item {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public int Count { get; set; }

        public Item() {

        }
        public Item(double price, string name, ProductType type) {
            Price = price;
            Name = name;
            Type = type;
        }

        public void AddAmount(int amount) {
            if (amount <= 0)
                throw new ArgumentException();
            Count += amount;
        }

        public bool SubtractAmount(int amount) {
            if (amount <= 0 || amount > Count)
                throw new ArgumentException();
            int first = Count;
            Count -= amount;
            return Count < first;
        }

        public bool CheckAvailability(int amount) {
            return amount <= Count;
        }
    }
}
