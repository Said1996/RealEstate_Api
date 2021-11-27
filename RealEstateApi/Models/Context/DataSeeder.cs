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
                Email = "said@outlook.com",
                UserName = "said@outlook.com",
                NormalizedEmail = "said@outlook.com".ToUpper(),
                NormalizedUserName = "said@outlook.com".ToUpper(),
                PhoneNumber = "01231231322",
                EmailConfirmed = true

            };
            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(adminUser, "aA@44781680").Wait();

            }
            if (!roleManager.Roles.Any())
            {


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

                roleManager.CreateAsync(role).Wait();

                roleManager.CreateAsync(role2).Wait();
                roleManager.CreateAsync(role3).Wait();

                userManager.AddToRoleAsync(adminUser, "Admin").Wait();

            }
        }
    }
}
