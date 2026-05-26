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
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}/Map")]
    [EnableRateLimiting("fixed")]
    public class MapController : BaseController
    {
        private readonly IDataService _dataService;
        public MapController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }
        // GET /v1/Map  latest position per active tanker
        // GET /v2/Map?sinceHours=24
        [HttpGet]
        public async Task<IActionResult> GetMap([FromQuery] int sinceHours = 168)
        {
            if (sinceHours < 1 || sinceHours > 8760) sinceHours = 168;
            try
            {
                var positions = await _dataService.GetLatestVesselPositionsAsync(sinceHours);
                var dto = positions.Select(v => new VesselMapPositionDTO
                {
                    Tanker_Id = v.Tanker_Id,
                    Mmsi = v.Mmsi,
                    Vessel_Name = v.Vessel_Name,
                    Ship_Type = v.Ship_Type,
                    Flag = v.Flag,
                    Latitude = v.Latitude,
                    Longitude = v.Longitude,
                    Timestamp_Utc = v.Timestamp_Utc,
                    Sog = v.Sog,
                    Cog = v.Cog,
                    Heading = v.Heading,
                    Navigational_Status = v.Navigational_Status,
                    Is_Anomalous = v.Flag == "UN"
                }).ToList();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR MapController", error = ex.ToString() });
            }
        }
    }
}