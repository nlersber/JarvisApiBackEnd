using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ActionResult Post([FromBody] OrderDTO orderDto) {
            //Stuff

            Order order = new Order();
            order.User = userRepo.GetBy(orderDto.User);
            order.Time = orderDto.Date;
            order.UserId = order.User.id;

            IList<OrderItemWrapper> items = new List<OrderItemWrapper>();

            foreach (OrderItemDTO item in orderDto.Items)
                items.Add(new OrderItemWrapper(itemRepo.GetById(item.Id), item.Count));

            order.ItemsList = items;

            if (!order.CheckAvailability())
                return new BadRequestResult();

            try {
                processOrder(order);
            }
            catch (ArgumentException e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return new BadRequestResult();
            }

            return new OkResult();
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult Get(string name) {
            var result = orderRepo.GetByUserId(userRepo.GetBy(name).id);
            return new OkObjectResult(result);
        }

        private void processOrder(Order order) {
            User user = order.User;

            IList<OrderItemWrapper> list = order.ItemsList;

            userRepo.SubtractBalance(user.id, order.GetTotal());

            foreach (var item in list)
                itemRepo.SubtractCountFromStock(item.Item.Id, item.Amount);

            orderRepo.Add(order);


            orderRepo.SaveChanges();
            userRepo.SaveChanges();
            itemRepo.SaveChanges();
        }


    }


}
