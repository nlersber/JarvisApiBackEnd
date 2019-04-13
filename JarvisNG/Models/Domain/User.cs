using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class User {
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public double Balance { get; set; }

        public bool IsAdmin { get; set; }

        public string Password { get; set; }




        public User() {

        }

        public User(int id, string name, double balance, bool isAdmin, string password) {
            this.id = id;
            Name = name;
            Balance = balance;
            IsAdmin = isAdmin;
            Password = password;
        }

        public User(bool isAdmin, int id, string name, string password) {
            IsAdmin = isAdmin;
            this.id = id;
            Name = name;
            Balance = 0;
            Password = password;
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
