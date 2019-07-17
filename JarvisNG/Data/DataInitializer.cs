using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace JarvisNG.Data {
    public class DataInitializer {
        private readonly DataContext context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly User[] users = new User[] {
            User.DEFAULT,
            new User {id = 2, Name = "Test", Balance = 0, IsAdmin = true, Email = "Test1@Test.test"},
            new User {id = 3, Name = "Test2", Balance = 0, IsAdmin = false, Email = "Test2@Test.test"},
            new User {id = 4, Name = "Test3", Balance = 0, IsAdmin = false, Email = "Test3@Test.test"},
            new User {id = 5, Name = "Test4", Balance = 0, IsAdmin = false, Email = "Test4@Test.test"},
            new User {id = 6, Name = "Test5", Balance = 0, IsAdmin = false, Email = "Test5@Test.test"},
            new User {id = 7, Name = "Test6", Balance = 0, IsAdmin = false, Email = "Test6@Test.test"}
        };

        public DataInitializer(DataContext context, UserManager<IdentityUser> userManager) {
            this.context = context;
            _userManager = userManager;
        }

        public async Task InitializeData() {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {
                foreach (var user in users) {
                    if (!context.ShopUsers.Any(s => s.id == user.id))
                        context.ShopUsers.Add(user);
                    await CreateUser(user.Name, user.Password, user.Email);
                }



                context.SaveChanges();
            }
        }

        private async Task CreateUser(string Name, string Password, string email) {
            var user = new IdentityUser { UserName = Name, Email = email };
            await _userManager.CreateAsync(user, Password);
        }
    }
}