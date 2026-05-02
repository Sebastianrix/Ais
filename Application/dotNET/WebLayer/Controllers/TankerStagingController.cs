//using WebLayer.DTOs;
//using DataLayer;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Routing;

//using WebLayer.DTOs;


//namespace WebLayer.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    [Route("api/v1/[controller]")]
//  public class TankerStagingController : BaseController    {
//private readonly IDataService _dataService;

//    public TankerPositionsController(IDataService dataService, LinkGenerator linkGenerator)
//        : base(linkGenerator)
// {        _dataService = dataService;
//   }




////    //    // GET api/v1/tankerpositions
////    //    [HttpGet]
////    //    public IActionResult GetTankerPositions()
////    //    {
////    //        try
////    //        {
////    //            var tankerPositions = _dataService.GetTankerPositions();
////    //            var dto = tankerPositions.Select(tp => new TankerPositionDTO
////    //            {
////    //                Position_Id = tp.Position_Id,
////    //                Tanker_Id = tp.Tanker_Id,
////    //                Voyage_Id = tp.Voyage_Id,
////    //                Staging_Id = tp.Staging_Id,
////    //                Timestamp = tp.Timestamp,
////    //                Longitude = tp.Longitude,
////    //                Latitude = tp.Latitude,
////    //                Raw_Imo = tp.Raw_Imo,
////    //                Imo_Status = tp.Imo_Status,
////    //                Raw_Mmsi = tp.Raw_Mmsi,
////    //                Mmsi_Status = tp.Mmsi_Status,
////    //                Anomaly_Flag = tp.Anomaly_Flag,
////    //                Navigational_Status = tp.Navigational_Status,
////    //                Rot = tp.Rot,
////    //                Sog = tp.Sog,
////    //                Cog = tp.Cog,
////    //                Heading = tp.Heading,
////    //                Draught = tp.Draught,
////    //                Destination = tp.Destination,
////    //                Eta = tp.Eta,
////    //                Position_Fixing_Device = tp.Position_Fixing_Device,
////    //                Data_Source_Type = tp.Data_Source_Type
////    //            }).ToList();

////             return Ok(dto);
////    //        }
////    //        //catch (Exception ex) {
////    //            //return StatusCode(500, new { message = "ERROR Check if the DTO rejected the db-entry cast mismatch with datatype", error = ex.Message });
////    //        //}
////    //      // } 
////    //catch (Exception ex) {
////    //Console.WriteLine($"[ERR] {ex}"); 
//return StatusCode(500, new { message = "ERROR Check if the DTO rejected the db-entry cast mismatch with datatype", error = ex.Message });
////    //      }
//}
//}
//}
