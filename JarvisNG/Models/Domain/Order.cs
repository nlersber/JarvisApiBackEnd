using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class Order {
        public IList<OrderItemWrapper> ItemsList { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public DateTime Time { get; set; }

        public Order() {

        }

        public Order(Dictionary<Item, int> Items, User User, DateTime Time) {
            this.User = User;
            this.Time = Time;
            flatten(Items);
        }

        public Order(IList<OrderItemWrapper> items) {
            ItemsList = items;
        }

        private void flatten(Dictionary<Item, int> Items) {
            ItemsList = new List<OrderItemWrapper>();

            foreach (var item in Items)
                for (int i = 0; i < item.Value; i++)
                    ItemsList.Add(new OrderItemWrapper(item.Key, item.Value));
        }

        public bool CheckAvailability() {
            return ItemsList.All(s => s.Item.CheckAvailability(s.Amount));
        }

        public double GetTotal() {
            double total = 0;
            foreach (var entry in ItemsList)
                total += (entry.Item.Price * entry.Amount);
            return total;

        }



        public IEnumerable<Item> GetUnavailables() {
            return ItemsList.Where(s => !s.Item.CheckAvailability(s.Amount)).Select(s => s.Item);
        }
    }


}
