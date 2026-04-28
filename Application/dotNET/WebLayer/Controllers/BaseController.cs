using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace WebLayer.Controllers

{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public BaseController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }
        protected string? GetUrl(string linkName, object args)
        {
            var uri = _linkGenerator.GetUriByName(HttpContext, linkName, args);
            return uri;
        }
    }
}
