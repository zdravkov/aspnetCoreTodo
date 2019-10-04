using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (Request.Headers.ContainsKey("Authorization")) {
                string authHeader = Request.Headers["Authorization"];
                String base64Decoded = "1";
                if (authHeader.Contains("Basic"))
                {
                    String creds = authHeader.Split(' ')[1];
                    byte[] data = System.Convert.FromBase64String(creds);
                    base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
                }

                if (base64Decoded.Contains("test")) {
                    return Ok(new string[] { "value1", "value2", authHeader, base64Decoded });
                }
                return Unauthorized();
            }

            if (Request.Headers.ContainsKey("X-API-Key"))
            {
                string authHeader = Request.Headers["X-API-Key"];

                if (authHeader.Contains("test"))
                {
                    return Ok();
                }

                return Unauthorized();
            }

            if (Request.Query.ContainsKey("X-API-Key"))
            {
                string authHeader = Request.Query["X-API-Key"];

                if (authHeader.Contains("test"))
                {
                    return Ok();
                }

                return Unauthorized();
            }

            return new string[] { "value1", "value2" };
        }
    }
}
