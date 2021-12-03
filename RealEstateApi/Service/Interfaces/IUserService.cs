using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using System.Threading.Tasks;

namespace RealEstateApi.Service.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRoleToUserAsync(AddRoleModel model);
        Task UpdateUserAsync(UserModel userModel);
        Task<UserModel> GetUserAsync(string id);
        Task CreateRoleAsync(string name);
    }
}
