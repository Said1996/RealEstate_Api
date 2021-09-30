using RealEstateApi.Models;
using RealEstateApi.Models.Request;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Service.Interfaces
{
    public interface IRealEstateService
    {
        Task<List<RealEstate>> GetAllRealEstateAsync(PaginationParameter paginationParameter);
        Task<RealEstate> GetRealEstateAsync(int id);
        Task<RealEstate> PostRealEstateAsync(RealEstate realEstate);
        Task<bool> DeleteRealEstateAsync(int id);
        Task<RealEstate> UpdateRealEstateAsync(RealEstate realEstate);
        IQueryable<RealEstate> Search(FilterParameter filterParameter, PaginationParameter paginationParameter);
        int TotalPages(int totalCount, int pageSize);
    }
}
