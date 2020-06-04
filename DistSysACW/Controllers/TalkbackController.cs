using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSysACW.Controllers
{
    public class TalkBackController : BaseController
    {
        public TalkBackController(Models.UserContext context) : base(context) { }

        [ActionName("Hello")]
        public ActionResult Get()
        {
            // Response.
            return Ok("Hello World");
        }

        [ActionName("Sort")]
        public IActionResult Get([FromQuery]string[] integers)
        {
            try
            {
                int[] validchars = new int[integers.Length];
                // Validate chars
                for (int i = 0; i < integers.Length; i++)
                {
                    int x = Int32.Parse(integers[i]);
                    validchars[i] = x;
                }
                
                // Sort valid
                Array.Sort(validchars);
                // Valid - Response.
                return Ok(validchars);
            }
            // Invalid - Response.
            catch (FormatException)
            {
                return BadRequest("Bad Request");
            }
            // Empty - Response.
            catch (ArgumentNullException)
            {
                return Ok("[]");
            }

        }
    }
}
