using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JarvisNG.Controllers {
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase {
        private readonly IItemRepository itemRepo;
        private readonly IUserRepository userRepo;

        public ItemController(IItemRepository itemRepo, IUserRepository userRepo) {
            this.itemRepo = itemRepo;
            this.userRepo = userRepo;
        }

        [HttpGet]
        public IEnumerable<Item> GetItems() {
            return itemRepo.GetAll().OrderBy(s => s.Category).ThenBy(s => s.Price).ThenBy(s => s.Name);
        }

        [HttpGet("{id}")]
        public IEnumerable<Item> GetByType(int id) {
            return itemRepo.GetByProductType((ProductType)id).OrderBy(s => s.Price).ThenBy(s => s.Name);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ItemDTO Item) {
            this.itemRepo.UpdateItem(new Item { Name = Item.Name, Id = Item.Id, Category = (ProductType)Item.Category, Price = Item.Price, Count = Item.Count });
            return new OkResult();
        }


    }
}
