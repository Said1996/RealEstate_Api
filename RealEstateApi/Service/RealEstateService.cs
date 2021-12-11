using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;
using RealEstateApi.Models.Context;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using RealEstateApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Service
{
    public class RealEstateService : IRealEstateService
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public RealEstateService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<RealEstateResponse>> GetAllRealEstateAsync(PaginationParameter paginationParameter)
        {
            return await appDbContext.RealEstates.Select(s => new RealEstateResponse()
            {
                Id = s.Id,
                Name = s.Name,
                PhotoPath = s.PhotoPath,
                Description = s.Description,
                Address = s.Address,
                Country = s.Country,
                City = s.City,
                Price = s.Price,
                Space = s.Space,
                BathroomNum = s.BathroomNum,
                BedroomNum = s.BedroomNum,
                KitchenNum = s.KitchenNum,
                PropertyType = s.PropertyType,
                OfferType = s.OfferType,
                SwimmingPool = s.SwimmingPool,
                SecuritySystem = s.SecuritySystem,
                Garden = s.Garden,
                UserName = s.User.Name,
                UserPhotoPath = s.User.PhotoPath,
                UserPhoneNumber = s.User.PhoneNumber,
            })
                    .Skip(paginationParameter.PageSize * (paginationParameter.PageNumber - 1))
                    .Take(paginationParameter.PageSize)
                    .ToListAsync();
        }

        public async Task<RealEstateResponse> GetRealEstateAsync(int id)
        {
            return await appDbContext.RealEstates.Select(s => new RealEstateResponse()
            {
                Id = s.Id,
                Name = s.Name,
                PhotoPath = s.PhotoPath,
                Description = s.Description,
                Address = s.Address,
                Country = s.Country,
                City = s.City,
                Price = s.Price,
                Space = s.Space,
                BathroomNum = s.BathroomNum,
                BedroomNum = s.BedroomNum,
                KitchenNum = s.KitchenNum,
                PropertyType = s.PropertyType,
                OfferType = s.OfferType,
                SwimmingPool = s.SwimmingPool,
                SecuritySystem = s.SecuritySystem,
                Garden = s.Garden,
                UserName = s.User.Name,
                UserPhotoPath = s.User.PhotoPath,
                UserPhoneNumber = s.User.PhoneNumber,
            })
            .FirstOrDefaultAsync();
        }

        public async Task<RealEstateModel> PostRealEstateAsync(RealEstateModel realEstateModel, string userId)
        {
            var realEstate = mapper.Map<RealEstate>(realEstateModel);
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            realEstate.User = user;
            await appDbContext.RealEstates.AddAsync(realEstate);
            await appDbContext.SaveChangesAsync();
            return realEstateModel;
        }

        public async Task<bool> DeleteRealEstateAsync(int id)
        {
            var result = appDbContext.Remove(new RealEstate() { Id = id });
            await appDbContext.SaveChangesAsync();
            return true;

        }

        public async Task<RealEstateModel> UpdateRealEstateAsync(RealEstateModel realEstateModel)
        {
            var realEstate = await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == realEstateModel.Id);
            if (realEstate == null)
                return null;

            realEstate = mapper.Map<RealEstate>(realEstateModel);

            await appDbContext.SaveChangesAsync();

            return realEstateModel;
        }

        public (IQueryable<RealEstateResponse> query, PaginationInformation pagination) Search(FilterParameter filterParameter, PaginationParameter paginationParameter)
        {


            var query = appDbContext.RealEstates.Select(s => new RealEstateResponse()
            {
                Id = s.Id,
                Name = s.Name,
                PhotoPath = s.PhotoPath,
                Description = s.Description,
                Address = s.Address,
                Country = s.Country,
                City = s.City,
                Price = s.Price,
                Space = s.Space,
                BathroomNum = s.BathroomNum,
                BedroomNum = s.BedroomNum,
                KitchenNum = s.KitchenNum,
                PropertyType = s.PropertyType,
                OfferType = s.OfferType,
                SwimmingPool = s.SwimmingPool,
                SecuritySystem = s.SecuritySystem,
                Garden = s.Garden,
                UserName = s.User.Name,
                UserPhotoPath = s.User.PhotoPath,
                UserPhoneNumber = s.User.PhoneNumber,
            });
            query = Filters(filterParameter, query);
            query.OrderBy(r => r.Address)
               .Skip(paginationParameter.PageSize * (paginationParameter.PageNumber - 1))
               .Take(paginationParameter.PageSize);

            var metadata = new PaginationInformation()
            {
                PageNumber = paginationParameter.PageNumber,
                TotalCount = query.Count(),
                TotalPages = TotalPages(query.Count(), paginationParameter.PageSize)
            };

            return (query, metadata);
        }

        private int TotalPages(int totalCount, int pageSize)
        {
            var result = 0;
            var totalPages = Math.DivRem(totalCount, pageSize, out result);
            if (result != 0)
                totalPages++;
            return totalPages;
        }

        private IQueryable<RealEstateResponse> Filters(FilterParameter filterParameter, IQueryable<RealEstateResponse> query)
        {
            if (!string.IsNullOrEmpty(filterParameter?.Search))
            {
                var titleQuery = query.Where(r => r.Name.Contains(filterParameter.Search));

                var countryQuery = query.Where(r => r.Country.Contains(filterParameter.Search));
                var cityQuery = query.Where(r => r.City.Contains(filterParameter.Search));
                var addressQuery = query.Where(r => r.Address.Contains(filterParameter.Search));

                query = titleQuery.Union(countryQuery).Union(cityQuery).Union(addressQuery);

            }

            if (!string.IsNullOrEmpty(filterParameter?.Address))
                query = query.Where(r => r.Address == filterParameter.Address);

            if (!string.IsNullOrEmpty(filterParameter?.City))
                query = query.Where(r => r.City == filterParameter.City);

            if (!string.IsNullOrEmpty(filterParameter?.Country))
                query = query.Where(r => r.Country == filterParameter.Country);

            if (filterParameter?.Price != null)
                query = query.Where(r => r.Price == filterParameter.Price);

            if (filterParameter?.PriceFrom != null)
                query = query.Where(r => r.Price >= filterParameter.PriceFrom);

            if (filterParameter?.PriceTo != null)
                query = query.Where(r => r.Price <= filterParameter.PriceTo);

            if (filterParameter?.MinSpace != null)
                query = query.Where(r => r.Space >= filterParameter.MinSpace);

            if (filterParameter?.MaxSpace != null)
                query = query.Where(r => r.Space <= filterParameter.MaxSpace);

            if (filterParameter?.KitchenNum != null)
                query = query.Where(r => r.KitchenNum == filterParameter.KitchenNum);

            if (filterParameter?.BedroomNum != null)
                query = query.Where(r => r.BedroomNum == filterParameter.BedroomNum);

            if (filterParameter?.BathroomNum != null)
                query = query.Where(r => r.BathroomNum == filterParameter.BathroomNum);

            if (filterParameter?.PropertyType != null)
                query = query.Where(r => r.PropertyType == filterParameter.PropertyType);

            if (filterParameter?.OfferType != null)
                query = query.Where(r => r.OfferType == filterParameter.OfferType);

            if (filterParameter?.SwimmingPool != null)
                query = query.Where(r => r.SwimmingPool == filterParameter.SwimmingPool);

            if (filterParameter?.SecuritySystem != null)
                query = query.Where(r => r.SecuritySystem == filterParameter.SecuritySystem);

            if (filterParameter?.Garden != null)
                query = query.Where(r => r.Garden == filterParameter.Garden);

            return query;
        }


    }
}
