﻿using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;
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

        public RealEstateService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<RealEstate>> GetAllRealEstateAsync(PaginationParameter paginationParameter)
        {
            return await appDbContext.RealEstates
                    .Skip(paginationParameter.PageSize * (paginationParameter.PageNumber - 1))
                    .Take(paginationParameter.PageSize)
                    .ToListAsync();
        }

        public async Task<RealEstate> GetRealEstateAsync(int id)
        {
            return await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RealEstate> PostRealEstateAsync(RealEstate realEstate)
        {
            await appDbContext.RealEstates.AddAsync(realEstate);
            appDbContext.SaveChanges();
            return realEstate;
        }

        public async Task<bool> DeleteRealEstateAsync(int id)
        {
            var realEstate = await GetRealEstateAsync(id);
            if (realEstate == null)
                return false;
            
            appDbContext.RealEstates.Remove(realEstate);
            appDbContext.SaveChanges();
            return true;
            
        }

        public async Task<RealEstate> UpdateRealEstateAsync(RealEstate realEstate)
        {
            var realEstateToModify = await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == realEstate.Id);
            if (realEstateToModify == null)
                return null;

            realEstateToModify.Name = realEstate.Name;
            realEstateToModify.Description = realEstate.Description;
            realEstateToModify.Price = realEstate.Price;
            realEstateToModify.RoomNum = realEstate.RoomNum;
            realEstateToModify.KitchenNum = realEstate.KitchenNum;
            realEstateToModify.PropertyType = realEstate.PropertyType;
            realEstateToModify.Space = realEstate.Space;
            realEstateToModify.SwimmingPool = realEstate.SwimmingPool;
            realEstateToModify.SecuritySystem = realEstate.SecuritySystem;
            realEstateToModify.OfferType = realEstate.OfferType;
            realEstateToModify.BathroomNum = realEstate.BathroomNum;
            realEstateToModify.City = realEstate.City;
            realEstateToModify.Garden = realEstate.Garden;

            await appDbContext.SaveChangesAsync();

            return realEstateToModify;
        }

        public IQueryable<RealEstate> Search(FilterParameter filterParameter, PaginationParameter paginationParameter)
        {
            var query = appDbContext.RealEstates.AsQueryable();
            query = Filters(filterParameter, query);
           
            return query;
        }

        public int TotalPages(int totalCount, int pageSize)
        {
            var result = 0;
            var totalPages = Math.DivRem(totalCount, pageSize, out result);
            if (result != 0)
                totalPages++;
            return totalPages;
        }

        private IQueryable<RealEstate> Filters(FilterParameter filterParameter, IQueryable<RealEstate> query)
        {
            if (!string.IsNullOrEmpty(filterParameter?.QueryName))
            {
                query = query.Where(r => r.Name.Contains(filterParameter.QueryName));
            }

            if (filterParameter.City != null)
            {
                query = query.Where(r => r.City == filterParameter.City);
            }
            return query;
        }

        
    }
}