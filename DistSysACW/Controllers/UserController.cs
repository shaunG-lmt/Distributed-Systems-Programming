using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DistSysACW.Models;
using Newtonsoft.Json;

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
            else if (UserDatabaseAccess.CheckUsername(username))
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
            else if (UserDatabaseAccess.CheckUsername(username))
            {

                return StatusCode(403, "Oops. This username is already in use. Please try again with a new username.");
                //return Forbid();
                //
            }
            // Add the new user
            else
            {
                return Ok(UserDatabaseAccess.NewUser(username));
            }    
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("RemoveUser")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string username)
        {
            // Get User from ApiKey of request.
            User foundUser = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);

            // Add Log. 
            string request = foundUser.UserName + " requested /User/RemoveUser/" + username;
            UserDatabaseAccess.AddLog(request, this.Request.Headers["ApiKey"]);
            
            if (UserDatabaseAccess.CheckUserApiKey(this.Request.Headers["ApiKey"])) // Tried to get apikey using identity set up in authmiddleware.cs, couldnt access claims and there was an empty identity in there as well.
            {
                User user = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);
                if(user.UserName == username)
                {
                    UserDatabaseAccess.RemoveUser(user);
                    return Ok(true);
                }
            }
            return Ok(false);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("ChangeRole")]
        [HttpPost]
        public IActionResult PostChangeRole([FromBody] JSONContract JSONBodyResult)
        {
            // Get User from ApiKey of request.
            User foundUser = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);

            // Add Log. 
            string request = foundUser.UserName + " requested /User/RemoveRole/ with a body of: " + JSONBodyResult;
            UserDatabaseAccess.AddLog(request, this.Request.Headers["ApiKey"]);

            string result = UserDatabaseAccess.ChangeRole(JSONBodyResult);
            if (result.StartsWith("NOT"))
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
