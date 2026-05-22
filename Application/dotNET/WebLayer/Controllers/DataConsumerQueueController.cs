using Asp.Versioning;
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
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class DataConsumerQueueController : BaseController
    {
        private readonly IDataService _dataService;

        public DataConsumerQueueController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService;
        }

        // GET /dataconsumerqueue
        [HttpGet]
        public IActionResult GetQueue()
        {
            try
            {
                var queue = _dataService.GetDataConsumerQueue();
                var dto = queue.Select(q => new DataConsumerQueueDTO
                {
                    Queue_Id = q.Queue_Id,
                    Source_Batch_Date = q.Source_Batch_Date,
                    Priority = q.Priority,
                    Requester = q.Requester,
                    Status = q.Status,
                    Created_At = q.Created_At
                }).ToList();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERR] {ex}");
                return StatusCode(500, new { message = "ERROR DataConsumerQueueController", error = ex.Message });
            }
        }
    }
}