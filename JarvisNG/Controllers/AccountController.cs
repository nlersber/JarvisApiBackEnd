using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JarvisNG.DTO;
using JarvisNG.Models.Domain;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JarvisNG.Controllers {
    [Route("api/[controller]")]
    public class AccountController : Controller {

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IUserRepository userRepo;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IUserRepository userRepo, IConfiguration config) {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userRepo = userRepo;
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken([FromBody]LoginDTO model) {
            bool isEmail = model.Username.Contains('@');
            var user = (isEmail ? await userManager.FindByEmailAsync(model.Username) : await userManager.FindByNameAsync(model.Username));

            if (user != null) {
                var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded) {
                    string token = GetToken(user);
                    return Created("", token); //returns only the token
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register([FromBody] RegisterDTO model) {
            IdentityUser iuser = new IdentityUser { UserName = model.Username, Email = model.Email };
            User user = new User {
                Email = model.Email,
                Name = model.Username
            };
            var result = await userManager.CreateAsync(iuser, model.Password);
            if (result.Succeeded) {
                userRepo.AddUser(user);
                userRepo.SaveChanges();
                string token = GetToken(iuser);
                return Created("", token);
            }
            return BadRequest();
        }



        // GET api/<controller>/5
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string name) {
            var user = await userManager.FindByNameAsync(name);
            return new OkObjectResult(user);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("checkadmin")]
        public async Task<ActionResult<Boolean>> CheckIsAdmin(string name) {
            var user = await userManager.FindByNameAsync(name);
            Boolean isAdmin = userRepo.IsAdmin(name);
            return new OkObjectResult((user != null && isAdmin));//Returns if user could be found and user is an admin
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("managementusers")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers() {
            return new OkObjectResult(userRepo.GetUsers());
        }

        [HttpGet("getuser")]
        public ActionResult<UserDTO> GetUserData(string name) {
            var iuser = this.userRepo.GetBy(name);
            UserDTO user = new UserDTO { Name = iuser.Name, Email = iuser.Email, Balance = iuser.Balance };
            return new OkObjectResult(user);
        }


        private String GetToken(IdentityUser user) {
            //Create the token
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, user.Email), new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}

