using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {

        private readonly string savedKeycode = "bond007";

        // Dependency Injection tyyli
        private readonly NorthwindOriginalContext db;

        public DocumentationController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }


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



