using Asp.Versioning;
using DataLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Linq.Expressions;
using WebLayer.DTOs;
using WebLayer.DTOs;


namespace WebLayer.Controllers
{

    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class TankerPositionsV1Controller : BaseController
    {
        private readonly IDataService _dataService;

        public TankerPositionsV1Controller(IDataService dataService, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTankerPositions()
        {
            try
            {
                var results = await _dataService.GetTankerPositionsAsync(page: 1, pageSize: 50, null, null, null);
                return Ok(new PagedResult<TankerPositionDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(tp => new TankerPositionDTO
                    {
                        Position_Id = tp.Position_Id,
                        Tanker_Id = tp.Tanker_Id,
                        Voyage_Id = tp.Voyage_Id,
                        Staging_Id = tp.Staging_Id,
                        Timestamp = tp.Timestamp,
                        Longitude = tp.Longitude,
                        Latitude = tp.Latitude,
                        Raw_Imo = tp.Raw_Imo,
                        Imo_Status = tp.Imo_Status,
                        Raw_Mmsi = tp.Raw_Mmsi,
                        Mmsi_Status = tp.Mmsi_Status,
                        Anomaly_Flag = tp.Anomaly_Flag,
                        Navigational_Status = tp.Navigational_Status,
                        Rot = tp.Rot,
                        Sog = tp.Sog,
                        Cog = tp.Cog,
                        Heading = tp.Heading,
                        Draught = tp.Draught,
                        Destination = tp.Destination,
                        Eta = tp.Eta,
                        Position_Fixing_Device = tp.Position_Fixing_Device,
                        Data_Source_Type = tp.Data_Source_Type
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR TankerPositionsV1Controller", error = ex.ToString() });
                {

                }
            }
        }



        [ApiVersion("2.0")]
        [ApiController]
        [Route("[controller]")]

        public class TankerPositionsV2Controller : BaseController
        {
            private readonly IDataService _dataService;

            public TankerPositionsV2Controller(IDataService dataService, LinkGenerator linkGenerator)
                : base(linkGenerator)
            {
                _dataService = dataService;
            }




            // GET /tankerpositions
            [HttpGet]
            public async Task<IActionResult> GetTankerPositions(
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 50,
                [FromQuery] int? tankerId = null,
                [FromQuery] DateTime? startDate = null,
                [FromQuery] DateTime? endDate = null)
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 500) pageSize = 50;

                try
                {

                    var results = await _dataService.GetTankerPositionsAsync(
                        page,
                        pageSize,
                        tankerId,
                        startDate,
                        endDate);

                    return Ok(new PagedResult<TankerPositionDTO>
                    {
                        Page = results.Page,
                        PageSize = results.PageSize,
                        TotalItems = results.TotalItems,

                        Items = results.Items.Select(tp => new TankerPositionDTO
                        {
                            Position_Id = tp.Position_Id,
                            Tanker_Id = tp.Tanker_Id,
                            Voyage_Id = tp.Voyage_Id,
                            Staging_Id = tp.Staging_Id,
                            Timestamp = tp.Timestamp,
                            Longitude = tp.Longitude,
                            Latitude = tp.Latitude,
                            Raw_Imo = tp.Raw_Imo,
                            Imo_Status = tp.Imo_Status,
                            Raw_Mmsi = tp.Raw_Mmsi,
                            Mmsi_Status = tp.Mmsi_Status,
                            Anomaly_Flag = tp.Anomaly_Flag,
                            Navigational_Status = tp.Navigational_Status,
                            Rot = tp.Rot,
                            Sog = tp.Sog,
                            Cog = tp.Cog,
                            Heading = tp.Heading,
                            Draught = tp.Draught,
                            Destination = tp.Destination,
                            Eta = tp.Eta,
                            Position_Fixing_Device = tp.Position_Fixing_Device,
                            Data_Source_Type = tp.Data_Source_Type
                        }).ToList()
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERR] {ex}");
                    return StatusCode(500, new { message = "ERROR TankerPositionsV2Controller", error = ex.ToString() });
                }
            }
        }
    }
}