using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ASP.NET_Core_Web_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TankerPositionsController : BaseController
    {
        private readonly IDataService _dataService;

        public TankerPositionsController(IDataService dataService, LinkGenerator linkGenerator)
            : base(linkGenerator)
        {
            _dataService = dataService; 
        } 
    
    }
}
