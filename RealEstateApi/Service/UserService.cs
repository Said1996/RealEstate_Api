using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Models;
using RealEstateApi.Models.Context;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using RealEstateApi.Service.Interfaces;
using RealEstateApi.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApi.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT Jwt;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, AppDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            Jwt = jwt.Value;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Name = model.FirstName,

            };
            var userWithSameEmail = await userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }



        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await userManager.FindByEmailAsync(tokenRequestModel.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {tokenRequestModel.Email}.";
                return authenticationModel;
            }
            if (await userManager.CheckPasswordAsync(user, tokenRequestModel.Password))
            {

                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }

            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }


        public async Task<string> AddRoleToUserAsync(AddRoleModel model)
        {

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExists = await roleManager.RoleExistsAsync(model.Role);
                if (roleExists)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                    return $"Added {model.Role} to user {model.Email}.";
                }
                return $"Role {model.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }

        public async Task CreateRoleAsync(string name)
        {
            var newRole = new IdentityRole() { Name = name };
            await roleManager.CreateAsync(newRole);

        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var bytes = Encoding.UTF8.GetBytes(Jwt.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(bytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: Jwt.Issuer,
                audience: Jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }


    }
}
