using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JarvisNG.Controllers {
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {

        private readonly IItemRepository itemRepo;
        private readonly IUserRepository userRepo;
        private readonly IOrderRepository orderRepo;

        public OrderController(IItemRepository itemRepo, IUserRepository userRepo, IOrderRepository orderRepo) {
            this.itemRepo = itemRepo;
            this.userRepo = userRepo;
            this.orderRepo = orderRepo;
        }

        [HttpPost]
        public async void Post(OrderDTO orderDto) {
            //Stuff
            System.Diagnostics.Debug.WriteLine(orderDto.Items.ToString());
            Order order = new Order();
            order.User = userRepo.GetDefault();

            IList<OrderItemWrapper> items = new List<OrderItemWrapper>();

            foreach (OrderItemDTO item in orderDto.Items)
                items.Add(new OrderItemWrapper(itemRepo.GetById(item.Id), item.Count));

            order.ItemsList = items;

            order.Time = DateTime.Now;

            if (!order.CheckAvailability())
                return;

            try {
                processOrder(order);
            }
            catch (ArgumentException e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return;
            }
            System.Diagnostics.Debug.WriteLine("Placed Order in Back");

        }

        private void processOrder(Order order) {
            User user = order.User;
            System.Diagnostics.Debug.WriteLine(user == null ? "user is null" : user.ToString());
            System.Diagnostics.Debug.WriteLine(order == null ? "order is null" : order.ToString());

            IList<OrderItemWrapper> list = order.ItemsList;

            userRepo.SubtractBalance(user.id, order.GetTotal());

            foreach (var item in list)
                itemRepo.SubtractCountFromStock(item.Item.Id, item.Amount);

            orderRepo.Add(order);


            orderRepo.SaveChanges();
            userRepo.SaveChanges();
            itemRepo.SaveChanges();
        }

        //// GET: api/Order
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Order/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Order

    }
}
