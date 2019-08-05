using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JarvisNG.Controllers {
    [Route("api/[controller]")]
    public class AccountController : Controller {

        private IUserRepository userRepo;
        private UserManager<IdentityUser> userManager;

        public AccountController(IUserRepository userRepo, UserManager<IdentityUser> userManager) {
            this.userRepo = userRepo;
            this.userManager = userManager;
        }

        // GET api/<controller>/5
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string name) {
            var user = await userManager.FindByNameAsync(name);
            return new OkObjectResult(user);
        }

        [HttpGet("checkadmin")]
        public async Task<ActionResult<Boolean>> CheckIsAdmin(string name) {
            var user = await userManager.FindByNameAsync(name);
            Boolean isAdmin = userRepo.IsAdmin(name);
            return new OkObjectResult((user != null && isAdmin));//Returns if user could be found and user is an admin
        }

        [HttpGet("managementusers")]
        public ActionResult<IEnumerable<ManagementUserDTO>> GetUsers() {
            return new OkObjectResult(userRepo.GetUsers());
        }

       

    }
}
