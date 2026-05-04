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
    public class StatsController : BaseController
    {
        private readonly IDataService _dataService;

        public TankerPositionsController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }



        //    GET api/getstats
        [HttpGet]
        public ActionResult<StatsDTO> GetStats()
        {
            try
            {
                var stats = _dataService.GetStats();

                var dto = new StatsDTO
                {
                    TankerCount = stats.TankerCount,
                    PositionCount = stats.PositionCount,
                    TrackedTankerCount = stats.TrackedTankerCount,
                    StagingCount = stats.StagingCount,
                };

                Console.WriteLine(stats == null ? "NULL!" : "NOT NULL");
                return Ok(dto);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new
                {
                    message = "ERROR StatsController",
                    error = ex.Message
                });
            }
        }
    }
}
