﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class User {
        public int id { get; }
        public string Name { get; }

        public double Balance { get; set; }

        public readonly bool IsAdmin;



        public User() {

        }

        public User(int id, string name, double balance, bool isAdmin) {
            this.id = id;
            Name = name;
            Balance = balance;
            IsAdmin = isAdmin;
        }

        public User(bool isAdmin, int id, string name) {
            IsAdmin = isAdmin;
            this.id = id;
            Name = name;
            Balance = 0;
        }

        public void AddBalance(double amount) {
            if (amount <= 0)
                throw new ArgumentException();
            Balance += amount;
        }

        public bool SubtractBalance(double amount) {
            if (amount <= 0 || amount > Balance)
                throw new ArgumentException();
            double first = Balance;
            Balance -= amount;
            return Balance < first;
        }

    }
}