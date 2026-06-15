using Asp.Versioning;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using WebLayer.DTOs;

namespace WebLayer.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/TankerStaging")]
    [EnableRateLimiting("fixed")]
    public class TankerStagingV1Controller : BaseController
    {
        private readonly IDataService _dataService;

        public TankerStagingV1Controller(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }

        // GET v1/TankerStaging
        [HttpGet]
        public async Task<IActionResult> GetTankerStagingsV1()
        {
            try
            {
                var results = await _dataService.GetTankerStagingsAsync(
                    page: 1, pageSize: 50,
                    mmsi: null, imo: null, startDate: null, endDate: null);

                return Ok(new PagedResult<TankerStagingDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(TankerStagingMapper.ToDto).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR TankerStagingV1Controller", error = ex.ToString() });
            }
        }
    }

    /* v2 examples
    https://api.aismap.dk/v2/TankerStaging?page=1&pageSize=50
    https://api.aismap.dk/v2/TankerStaging?mmsi=219000123
    https://api.aismap.dk/v2/TankerStaging?imo=9123456&startDate=2026-01-01&endDate=2026-02-01
    https://api.aismap.dk/v2/TankerStaging?startDate=2026-01-01&endDate=2026-02-01&page=2&pageSize=100
    (startDate/endDate filter on source_batch_date)
    */

    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/TankerStaging")]
    [EnableRateLimiting("fixed")]
    public class TankerStagingV2Controller : BaseController
    {
        private readonly IDataService _dataService;

        public TankerStagingV2Controller(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }

        // GET v2/TankerStaging
        [HttpGet]
        public async Task<IActionResult> GetTankerStagingsV2(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] string? mmsi = null,
            [FromQuery] string? imo = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 500) pageSize = 50;

            try
            {
                var results = await _dataService.GetTankerStagingsAsync(
                    page, pageSize, mmsi, imo, startDate, endDate);

                return Ok(new PagedResult<TankerStagingDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(TankerStagingMapper.ToDto).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR TankerStagingV2Controller", error = ex.ToString() });
            }
        }
    }

    internal static class TankerStagingMapper
    {
        public static TankerStagingDTO ToDto(TankerStaging ts) => new TankerStagingDTO
        {
            Staging_Id = ts.Staging_Id,
            Timestamp_Raw = ts.Timestamp_Raw,
            Type_Of_Mobile = ts.Type_Of_Mobile,
            Mmsi = ts.Mmsi,
            Latitude_Raw = ts.Latitude_Raw,
            Longitude_Raw = ts.Longitude_Raw,
            Navigational_Status = ts.Navigational_Status,
            Rot_Raw = ts.Rot_Raw,
            Sog_Raw = ts.Sog_Raw,
            Cog_Raw = ts.Cog_Raw,
            Heading_Raw = ts.Heading_Raw,
            Imo = ts.Imo,
            Callsign = ts.Callsign,
            Vessel_Name = ts.Vessel_Name,
            Ship_Type = ts.Ship_Type,
            Cargo_Type = ts.Cargo_Type,
            Width_Raw = ts.Width_Raw,
            Length_Raw = ts.Length_Raw,
            Position_Fixing_Device = ts.Position_Fixing_Device,
            Draught_Raw = ts.Draught_Raw,
            Destination = ts.Destination,
            Eta_Raw = ts.Eta_Raw,
            Data_Source_Type = ts.Data_Source_Type,
            Size_A = ts.Size_A,
            Size_B = ts.Size_B,
            Size_C = ts.Size_C,
            Size_D = ts.Size_D,
            Source_File_Name = ts.Source_File_Name,
            Source_Batch_Date = ts.Source_Batch_Date,
            Created_At = ts.Created_At,
            Updated_At = ts.Updated_At
        };
    }
}