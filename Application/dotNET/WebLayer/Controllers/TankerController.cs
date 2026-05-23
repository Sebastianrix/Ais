using Asp.Versioning;
using DataLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using WebLayer.DTOs;
using WebLayer.DTOs;


namespace WebLayer.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("[controller]")]
    [EnableRateLimiting("fixed")]
    public class TankerController : BaseController
    {
        private readonly IDataService _dataService;

        public TankerController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }




        // GET api/tankers
        [HttpGet]
        public async Task<IActionResult> GetTankers(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] bool? isActive = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 500) pageSize = 50;

            try
            {
                var results = await _dataService.GetTankersAsync(page, pageSize, isActive);
                return Ok(new PagedResult<TankerDTO>
                {
                    Page = results.Page,
                    PageSize = results.PageSize,
                    TotalItems = results.TotalItems,
                    Items = results.Items.Select(t => new TankerDTO
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
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                {
                    return StatusCode(500, new { message = "Error GetTankers endpooint failed", error = ex.Message });
                }

            }
        }
    }
}
