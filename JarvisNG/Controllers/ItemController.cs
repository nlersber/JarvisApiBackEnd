using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using JarvisNG.Models.Domain.Enums;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JarvisNG.Controllers {
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
        public IEnumerable<Item> GetAll() {
            return itemRepo.GetAll().OrderBy(s => s.Type).ThenBy(s => s.Name);
        }

        [HttpGet]
        public IEnumerable<Item> GetByType(string type) {
            return itemRepo.GetByProductType((ProductType)Enum.Parse(typeof(ProductType), type)).OrderBy(s => s.Name);
        }
    }
}
