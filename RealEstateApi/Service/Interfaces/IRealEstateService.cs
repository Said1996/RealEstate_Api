using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Service.Interfaces
{
    public interface IRealEstateService
    {
        Task<List<RealEstateResponse>> GetAllRealEstateAsync(PaginationParameter paginationParameter);
        Task<RealEstateResponse> GetRealEstateAsync(int id);
        Task<RealEstateModel> PostRealEstateAsync(RealEstateModel realEstateModel, string userId);
        Task<bool> DeleteRealEstateAsync(int id);
        Task<RealEstateModel> UpdateRealEstateAsync(RealEstateModel realEstateModel);
        (IQueryable<RealEstateResponse> query, PaginationInformation pagination) Search(FilterParameter filterParameter, PaginationParameter paginationParameter);
    }
}
