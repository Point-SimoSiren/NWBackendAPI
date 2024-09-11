using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        [HttpGet]
        public string GetHelloMessage()
        {
            return "Hello World!";
        }


        [HttpPost]
        public int LaskeYhteen(int a, int b)
        {
            return a + b;
        }
    }
}

