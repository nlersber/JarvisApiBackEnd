using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;

namespace JarvisNG.Data.Repositories {
    public class OrderRepository : IOrderRepository {
        private readonly DataContext context;


        public OrderRepository(DataContext context) {
            this.context = context;
        }

        public IList<HistoryOrder> GetByUserId(int id) {
            IList<OrderDbDTO> orders = new List<OrderDbDTO>();
            IDictionary<int, string> dates = new Dictionary<int, string>();

            using (var command = context.Database.GetDbConnection().CreateCommand()) {
                command.CommandText =
                    $"SELECT o.Id ,i.[Name], oi.Amount, o.[Time] " +
                    $"FROM dbo.OrderItemWrapper oi " +
                    $"JOIN Items i ON oi.ItemId=i.Id " +
                    $"JOIN dbo.[Order] o ON oi.OrderId=o.Id " +
                    $"WHERE o.UserId={id} ORDER BY o.Id, oi.ItemId";
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader()) {
                    OrderDbDTO fromDb;
                    while (result.Read()) {
                        fromDb = new OrderDbDTO();
                        fromDb.Id = result.GetInt32(0);
                        fromDb.Name = result.GetString(1);
                        fromDb.Amount = result.GetInt32(2);
                        fromDb.Time = result.GetString(3);

                        if (!dates.ContainsKey(fromDb.Id))
                            dates.Add(fromDb.Id, fromDb.Time);

                        orders.Add(fromDb);
                    }
                }


            }


            var temp = orders.GroupBy(s => s.Id).OrderBy(s => s.Key).ToDictionary(s => s.Key, s => s.ToList())
                .Select(s => new HistoryOrder {
                    Items = s.Value.Select(t => { return new HistoryOrderItem { Name = t.Name, Amount = t.Amount }; })
                        .ToList(),
                    Date = dates[s.Key]
                }).ToList();

            return temp;
        }

        public void Add(Order order) {
            context.Add(order);
        }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
