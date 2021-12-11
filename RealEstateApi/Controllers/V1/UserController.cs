using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using RealEstateApi.Service.Interfaces;
using System.Linq;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUser()
        {
            var id = User.Claims.SingleOrDefault(c => c.Type == "uid").Value;
            var result = await userService.GetUserAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserModel userModel)
        {
            await userService.UpdateUserAsync(userModel);
            return Ok();
        }


        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterUserAsync(RegisterModel registerModel)
        {
            var result = await userService.RegisterAsync(registerModel);
            return Ok(result);
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<AuthenticationModel>> SignIn(TokenRequestModel tokenRequestModel)
        {
            var result = await userService.GetTokenAsync(tokenRequestModel);
            if (result.IsSuccessful)
                return Ok(result);
            else
                return BadRequest(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsync(string roleName)
        {
            await userService.CreateRoleAsync(roleName);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult<string>> AddRoleToUserAsync(AddRoleModel addRoleModel)
        {
            var result = await userService.AddRoleToUserAsync(addRoleModel);
            return Ok(result);
        }

    }
}
