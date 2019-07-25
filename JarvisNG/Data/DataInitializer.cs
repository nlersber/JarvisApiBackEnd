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


        public DataInitializer(DataContext context, UserManager<IdentityUser> userManager) {
            this.context = context;
            _userManager = userManager;
        }

        public async Task InitializeData() {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {
                foreach (var user in context.ShopUsers) {
                    if (!context.ShopUsers.Any(s => s.id == user.id))
                        context.ShopUsers.Add(user);
                    await CreateUser(user.Name, "Password", user.Email);
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