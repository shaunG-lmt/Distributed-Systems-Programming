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
        /// <summary>
        /// Constructs a TalkBack controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public TalkBackController(Models.UserContext context) : base(context) { }


        [ActionName("Hello")]
        public ActionResult Get()
        {
            #region TASK1
            // TODO: add api/talkback/hello response
            #endregion

            return Ok("Hello World");
        }

        [ActionName("Sort")]
        public IActionResult Get([FromQuery]string[] integers)
        {
            #region TASK1
            // TODO: 
            // sort the integers into ascending order
            // send the integers back as the api/talkback/sort response
            #endregion
            
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
                return Ok(validchars);
            }
            // Invalid chars
            catch (FormatException)
            {
                return BadRequest("Bad Request");
            }
            // Empty
            catch (ArgumentNullException)
            {
                return Ok("[]");
            }

        }
    }
}
