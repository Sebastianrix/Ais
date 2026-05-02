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
    public class TankerStagingController : BaseController
    {
        private readonly IDataService _dataService;

        public TankerStagingController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }




        //    GET api/TankerStaging
        [HttpGet]
        public IActionResult GetTankerStagings()
        {
            try
            {
                var tankerstaging = _dataService.GetTankerStagings();
                var dto = tankerstaging.Select(ts => new TankerStagingDTO
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
                  Vessel_Name = ts.Vessel_Name = ts.
                  Ship_Type = ts.Ship_Type,
                  Cargo_Type = ts.Cargo_Type,
                  Width_Raw  = ts.Width_Raw,
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
                  Source_Batch_Date  = ts.Source_Batch_Date,
                  Created_At  = ts.Created_At,
                  Updated_At = ts.Updated_At
    }).ToList();

                return Ok(dto);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new
                {
                    message = "ERROR Tanker¨StagingController",
                    error = ex.Message
                });
            }
        }
    }
}
