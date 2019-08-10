using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class User {
        public static User DEFAULT {
            get { return new User { id = 1, Name = "Default", Balance = 200, IsAdmin = true, Email = "Default@Test.test" }; }
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public double Balance { get; set; }

        public bool IsAdmin { get; set; }





        public User() {

        }

        public User(int id, string name, double balance, bool isAdmin) {
            this.id = id;
            Name = name;
            Balance = balance;
            IsAdmin = isAdmin;
            
        }

        public User(bool isAdmin, int id, string name, string password) {
            IsAdmin = isAdmin;
            this.id = id;
            Name = name;
            Balance = 100;
            
        }

        public void AddBalance(double amount) {
            if (amount <= 0)
                throw new ArgumentException();
            Balance += amount;
        }

        public bool SubtractBalance(double amount) {
            if (amount <= 0 || amount > Balance)
                throw new ArgumentException("Amount: " + amount + ", Balance: " + Balance);
            double first = Balance;
            Balance -= amount;
            return Balance < first;
        }

        public override string ToString() {
            return this.Name + " " + this.id;
        }

    }
}
