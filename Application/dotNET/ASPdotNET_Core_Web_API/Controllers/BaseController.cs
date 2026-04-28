using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API.Controllers

{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        private BaseController(LinkGenerator linkGenerator)
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
