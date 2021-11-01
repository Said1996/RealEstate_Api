using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Models.Request;
using RealEstateApi.Service.Interfaces;
using System.Threading.Tasks;

namespace RealEstateApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPost("registerModel")]
        public async Task<ActionResult> RegisterUserAsync(RegisterModel registerModel)
        {
            var result = await userService.RegisterAsync(registerModel);
            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
            var result = await userService.GetTokenAsync(tokenRequestModel);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("roleName:string")]
        public async Task<IActionResult> CreateRoleAsync(string roleName)
        {
            await userService.CreateRoleAsync(roleName);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult> AddRoleToUserAsync(AddRoleModel addRoleModel)
        {
            var result = await userService.AddRoleToUserAsync(addRoleModel);
            return Ok(result);
        }

    }
}
