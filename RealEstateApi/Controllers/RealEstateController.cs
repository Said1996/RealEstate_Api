﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealEstateApi.Models;
using RealEstateApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public RealEstateController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRealEstates()
        {
            try
            {
                return Ok(await appDbContext.RealEstates.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RealEstate>> GetRealEstate(int id)
        {
            try
            {
                var result = await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<RealEstate>> PostRealEstate(RealEstate realEstate)
        {
            try
            {
                if (realEstate == null)
                    return BadRequest();

                var rEstate = await appDbContext.RealEstates.AddAsync(realEstate);
                appDbContext.SaveChanges();

                return CreatedAtAction(nameof(GetRealEstate),
                    new { id = rEstate.Entity.Id }, realEstate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new RealEstate record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<RealEstate>> DeleteRealEstate(int id)
        {
            try
            {
                var realState = await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == id);
                if (realState == null)
                    return BadRequest();

                appDbContext.RealEstates.Remove(realState);
                await appDbContext.SaveChangesAsync();

                return realState;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RealEstate>> PutRealEstate(int id, RealEstate realEstate)
        {
            try
            {
                if (realEstate.Id != id)
                    return BadRequest();

                var realEstateToModify = await appDbContext.RealEstates.FirstOrDefaultAsync(r => r.Id == id);
                if (realEstateToModify == null)
                    return NotFound($"RealEstate with Id = {id} not found");

                

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error updating data");
            }
        }

        [HttpGet("{search}")]
        public ActionResult<IEnumerable<RealEstate>> Search([FromQuery]FilterParameter filterParameter, [FromQuery]PaginationParameter paginationParameter)
        {
            try
            {
                var result = appDbContext.RealEstates.AsQueryable();

                

                result = Filters(filterParameter);

                var metadata = new PaginationInformation()
                {
                    PageNumber = paginationParameter.PageNumber,
                    TotalCount = result.Count(),
                    TotalPages = (int)((double)result.Count() / (double)paginationParameter.PageSize)
                };


                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                if (result.Any())
                    return Ok(result);

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
                
            }
        }

        private IQueryable<RealEstate> Filters(FilterParameter filterParameter, IQueryable<RealEstate> result)
        {
            if (!string.IsNullOrEmpty(filterParameter?.QueryName))
            {
                result = appDbContext.RealEstates.Where(r => r.Name.Contains(filterParameter.QueryName));
            }

            if(filterParameter.City != null)
            {
                result.Where(r => r.City == filterParameter.City);
            }



            return result;
        }




    }
    
}
