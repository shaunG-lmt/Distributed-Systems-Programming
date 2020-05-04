using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DistSysACW.Models;

namespace DistSysACW.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Constructs a User controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public UserController(Models.UserContext context) : base(context) { }

        [ActionName("New")]
        [HttpGet]
        public IActionResult Get([FromQuery]string username)
        {
            // Blank input
            if (username == null)
            {
                return Ok("\"False - User Does Not Exist! Did you mean to do a POST to create a new user?\"");
            }
            // User found
            else if (UserDatabaseAccess.checkUsername(username))
            {
                return Ok("\"True - User Does Exist! Did you mean to do a POST to create a new user?\"");
            }
            // No User found
            else
            {
                return Ok("\"False - User Does Not Exist! Did you mean to do a POST to create a new user?\"");
            }
        }

        [ActionName("New")]
        [HttpPost]
        public IActionResult Post([FromBody] string username)
        {
            // Blank input
            if (username == null)
            {
                return BadRequest("Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json");
            }
            // User already exists
            else if (UserDatabaseAccess.checkUsername(username))
            {

                return StatusCode(403, "Oops. This username is already in use. Please try again with a new username.");
                //return Forbid();
                //
            }
            // Add the new user
            else
            {
                return Ok(UserDatabaseAccess.newUser(username));
            }    
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("Removeuser")]
        public IActionResult Delete([FromQuery] string username)
        {
            // Tried to do this through using identity set up in authmiddleware.cs, couldnt access claims and there was an empty identity in there as well.
            string apikey = this.Request.Headers["ApiKey"];
            if (UserDatabaseAccess.checkUserApiKey(apikey))
            {
                User user = UserDatabaseAccess.returnUserFromApiKey(apikey);
                if(user.UserName == username)
                {
                    UserDatabaseAccess.removeUser(user);
                    return Ok(true);
                }
            }
            return Ok(false);
        }


    }
}
