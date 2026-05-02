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
    public class TrackedTankerController : BaseController
    {
        private readonly IDataService _dataService;

        public TrackedTankerController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }




    //    GET api/trackedtankers
       [HttpGet]
        public IActionResult GetTrackedTanker()
        {
            try
            {
                var trackedtanker = _dataService.GetTrackedTankers();
                var dto = trackedtanker.Select(tt => new TrackedTankerDTO
                {
                   Tracked_Id = tt.Tracked_Id,
                   Imo  = tt.Imo,
                   Source_Trial = tt.Source_Trial,
                   Notes = tt.Notes,
                   Is_Active = tt.Is_Active,
                   Created_At = tt.Created_At,
                   Updated_At = tt.Updated_At

    }).ToList();

                return Ok(dto);
            }
 
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR TrackedTankerController", error = ex.Message
    });
            }
        }
    }
}
