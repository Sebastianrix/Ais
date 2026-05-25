using Asp.Versioning;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using WebLayer.DTOs;


    /// api.aismap.dk/v2/Tankers?mmsi=123456789 <- Put a real mmsi number 

namespace WebLayer.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/Tankers")]
    [EnableRateLimiting("fixed")]
    public class TankerV1Controller : BaseController
    {
        private readonly IDataService _dataService;

        public TankerV1Controller(IDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator) { _dataService = dataService; }
        // GET api.aismap.dk/v1/Tankers
        [HttpGet]
        public async Task<IActionResult> GetTankersV1()
        {
            try
            {
                var results = await _dataService.GetTankersAsync(page: 1, pageSize: 50);
                return Ok(new PagedResult<TankerDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(TankerMapper.ToDto).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                {
                    return StatusCode(500, new { message = "Error TannkersV2Controller", error = ex.ToString() });
                }

            }
        } }
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/Tankers")]
    [EnableRateLimiting("fixed")]
    public class TankerV2Controller : BaseController
    {
        private readonly IDataService _dataService;

        public TankerV2Controller(IDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator) { _dataService = dataService; }
        //GET api.aismap.dk/v2/Tankers
        [HttpGet]
        public async Task<IActionResult> GetTankersV2(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] bool? isActive = null,
        [FromQuery] string? imo = null,
        [FromQuery] string? mmsi = null,
        [FromQuery] string? search = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 500) pageSize = 50;
            try
            {
                var results = await _dataService.GetTankersAsync(page, pageSize, isActive, imo, mmsi, search);
                return Ok(new PagedResult<TankerDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(TankerMapper.ToDto).ToList()
                });
            }
            catch (Exception ex) { Console.WriteLine($"[ERR] {ex}"); return StatusCode(500, new { message = "ERROR TankerV2Controller", error = ex.ToString() }); }
        }
    }

    }

        internal static class TankerMapper
        {
            public static TankerDTO ToDto(Tanker t) => new TankerDTO
            {
                Tanker_Id = t.Tanker_Id,
                Imo = t.Imo,
                Mmsi = t.Mmsi,
                Vessel_Name = t.Vessel_Name,
                Callsign = t.Callsign,
                Ship_Type = t.Ship_Type,
                Cargo_Type = t.Cargo_Type,
                Type_Of_Mobil = t.Type_Of_Mobil,
                Width = t.Width,
                Length = t.Length,
                Size_A = t.Size_A,
                Size_B = t.Size_B,
                Size_C = t.Size_C,
                Size_D = t.Size_D,
                Flag = t.Flag,
                First_Seen_At = t.First_Seen_At,
                Last_Seen_At = t.Last_Seen_At,
                Is_Active = t.Is_Active,
                Created_At = t.Created_At,
                Updated_At = t.Updated_At
            };
        }
    



