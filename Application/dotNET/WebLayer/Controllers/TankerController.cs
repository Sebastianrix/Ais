using WebLayer.DTOs;
using DataLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

using WebLayer.DTOs;


namespace WebLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("api/v1/[controller]")]
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
        public IActionResult GetTankers()
        {
            try
            {
                var tanker = _dataService.GetTankers();
                var dto = tanker.Select(t => new TankerDTO
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


                }).ToList();

                return Ok(dto);
            }
            //catch (Exception ex) {
                //return StatusCode(500, new { message = "ERROR Check if the DTO rejected the db-entry cast mismatch with datatype", error = ex.Message });
            //}
          // } 
    catch (Exception ex) {
    Console.WriteLine($"[ERR] {ex}"); 
    return StatusCode(500, new { message = "Error GetTankers endpoint failed, this messeg is TankerController.", error = ex.Message });
          }
    }
}}
