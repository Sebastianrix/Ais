using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLayer.DTOs;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataDateArchiveController : BaseController
    {
        private readonly IDataService _dataService;

        public DataDateArchiveController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }

        // GET /datedatearchive
        [HttpGet]
        public IActionResult GetArchive()
        {
            try
            {
                var archive = _dataService.GetDataDateArchive();
                var dto = archive.Select(a => new DataDateArchiveDTO
                {
                    Source_Batch_Date = a.Source_Batch_Date,
                    Total_Rows = a.Total_Rows,
                    Tanker_Rows = a.Tanker_Rows,
                    Positions_Inserted = a.Positions_Inserted,
                    Archived_At = a.Archived_At
                }).ToList();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR DataDateArchiveController", error = ex.Message });
            }
        }
    }
}