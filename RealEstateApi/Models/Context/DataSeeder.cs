using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Models.Context
{
    public class DataSeeder
    {


        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var adminUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "said",
                Email = "said.amir.kattan@outlook.com",
                UserName = "said.amir.kattan@outlook.com",
                NormalizedEmail = "said.amir.kattan@outlook.com".ToUpper(),
                NormalizedUserName = "said.amir.kattan@outlook.com".ToUpper(),
                PhoneNumber = "01231231322",
                EmailConfirmed = true

            };



            var role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var role2 = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Moderator",
                NormalizedName = "Moderator".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var role3 = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };



            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(adminUser, "aaa111!!!AAA").Wait();
            }

            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(role).Wait();
                roleManager.CreateAsync(role2).Wait();
                roleManager.CreateAsync(role3).Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }


        }
    }
}
