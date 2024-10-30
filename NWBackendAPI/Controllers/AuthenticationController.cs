using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;
using NWBackendAPI.Services.Interfaces;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        // DI tyylisesti vastaanotetaan Interface
        private IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }


        // Tähän tulee Front endin kirjautumisyritys
        [HttpPost]
        public ActionResult Post([FromBody] Credentials cred)
        {
            var loggedUser = _authenticateService.Authenticate(cred.Username, cred.Password);

            if (loggedUser != null)
            {
                return Ok(loggedUser); // Palautus front endiin (sis. vain loggedInUser luokan mukaiset kentät)
            }

            return BadRequest("Käyttäjätunus tai salasana on virheellinen");
        }

    }
}
