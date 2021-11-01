using RealEstateApi.Models;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApi.Service.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRoleToUserAsync(AddRoleModel model);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task CreateRoleAsync(string name);
    }
}
