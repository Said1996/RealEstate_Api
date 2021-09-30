using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealEstateApi.Models;
using RealEstateApi.Models.Enums;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using RealEstateApi.Service.Interfaces;
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
        private readonly IRealEstateService realEstateService;

        public RealEstateController(IRealEstateService realEstateService)
        {

            this.realEstateService = realEstateService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRealEstates([FromQuery]PaginationParameter paginationParameter)
        {
            try
            {
                return Ok(await realEstateService.GetAllRealEstateAsync(paginationParameter));
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
                var result = await realEstateService.GetRealEstateAsync(id);

                if (result == null) 
                    return NotFound();
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

                var rEstate = await realEstateService.PostRealEstateAsync(realEstate);

                return CreatedAtAction(nameof(GetRealEstate),
                    new { id = rEstate.Id }, realEstate);
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
                var done = await realEstateService.DeleteRealEstateAsync(id);
                if (done == false)
                    return BadRequest();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RealEstate>> UpdateRealEstate(int id, RealEstate realEstate)
        {
            try
            {
                if (realEstate.Id != id)
                    return BadRequest();
                var realEstateToModify = await realEstateService.UpdateRealEstateAsync(realEstate);
                return realEstateToModify;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error updating data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<RealEstate>>> Search([FromQuery]FilterParameter filterParameter,
            [FromQuery]PaginationParameter paginationParameter)
        {
            try
            {
                var query = realEstateService.Search(filterParameter, paginationParameter);

                var result = await query.OrderBy(r => r.Name)
               .Skip(paginationParameter.PageSize * (paginationParameter.PageNumber - 1))
               .Take(paginationParameter.PageSize)
               .ToListAsync();

                var metadata = new PaginationInformation()
                {
                    PageNumber = paginationParameter.PageNumber,
                    TotalCount = result.Count(),
                    TotalPages = realEstateService.TotalPages(query.Count(), paginationParameter.PageSize)
                };

                if (result.Any())
                {
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(result);
                }


                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
                
            }
        }

 



    }
    
}
