using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {

        private readonly NorthwindOriginalContext db = new NorthwindOriginalContext();
        private readonly string savedKeycode = "bond007";

        [HttpGet("{keycode}")]
        public ActionResult GetDocumentation(string keycode)
        {
            if (keycode == savedKeycode)
            {
                var docs = db.Documentations.ToList();
                return Ok(docs);
            }
            else
            {
                return Unauthorized("keycode is not valid");
            }

        }
    }
}



