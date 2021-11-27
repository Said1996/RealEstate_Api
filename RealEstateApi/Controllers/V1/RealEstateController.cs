using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealEstateApi.Models.Request;
using RealEstateApi.Models.Response;
using RealEstateApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApi.Controllers.V1
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
        public async Task<ActionResult> GetAllRealEstates([FromQuery] PaginationParameter paginationParameter)
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
        public async Task<ActionResult<RealEstateResponse>> GetRealEstate(int id)
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

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<ActionResult<RealEstateModel>> PostRealEstate(RealEstateModel realEstateModel)
        {
            try
            {

                if (realEstateModel == null)
                    return BadRequest();

                var UserId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value;
                var rEstate = await realEstateService.PostRealEstateAsync(realEstateModel, UserId);

                return CreatedAtAction(nameof(GetRealEstate),
                    new { id = rEstate.Id }, realEstateModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new RealEstate record");
            }
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<RealEstateModel>> DeleteRealEstate(int id)
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

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RealEstateModel>> UpdateRealEstate(int id, RealEstateModel realEstateModel)
        {
            try
            {
                if (realEstateModel.Id != id)
                    return BadRequest();
                var realEstate = await realEstateService.UpdateRealEstateAsync(realEstateModel);
                return realEstate;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error updating data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<RealEstateResponse>>> Search([FromQuery] FilterParameter filterParameter,
            [FromQuery] PaginationParameter paginationParameter)
        {
            try
            {
                var result = realEstateService.Search(filterParameter, paginationParameter);

                if (result.query.Any())
                {
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.pagination));
                    return Ok(await result.query.ToListAsync());
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
