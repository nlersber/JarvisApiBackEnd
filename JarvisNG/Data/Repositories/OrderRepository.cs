using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Data.Repositories {
    public class OrderRepository : IOrderRepository {
        private readonly DataContext context;


        public OrderRepository(DataContext context) {
            this.context = context;
        }

        public Task<IList<Order>> GetByUserId(int id) {
            IList<Order> orders = new List<Order>();

            using (var command = context.Database.GetDbConnection().CreateCommand()) {
                command.CommandText = "SELECT * FROM [dbo].[Config_ViewsInPortal] WHERE DisplayedInPortal=1";
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader()) {
                }
            }

            return null;
        }

        public async void Add(Order order) {
            await context.AddAsync(order);
        }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
