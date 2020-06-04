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
        public UserController(Models.UserContext context) : base(context) { }

        [ActionName("New")]
        [HttpGet]
        public IActionResult Get([FromQuery]string username)
        {
            if (username == null)
            {
                // Blank input - Response.
                return Ok("\"False - User Does Not Exist! Did you mean to do a POST to create a new user?\"");
            }
            else if (UserDatabaseAccess.CheckUsername(username))
            {
                // User found - Response.
                return Ok("\"True - User Does Exist! Did you mean to do a POST to create a new user?\"");
            }
            else
            {
                // No user found - Response.
                return Ok("\"False - User Does Not Exist! Did you mean to do a POST to create a new user?\"");
            }
        }

        [ActionName("New")]
        [HttpPost]
        public IActionResult Post([FromBody] string username)
        {
            if (username == null)
            {
                // Blank input - Response
                return BadRequest("Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json");
            }
            else if (UserDatabaseAccess.CheckUsername(username))
            {
                // Username in use - Response.
                return StatusCode(403, "Oops. This username is already in use. Please try again with a new username.");
            }
            else
            {
                // User created - Response.
                return Ok(UserDatabaseAccess.NewUser(username));
            }    
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("RemoveUser")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string username)
        {
            // Action.
            User foundUser = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);

            // Logging. 
            string request = foundUser.UserName + " requested /User/RemoveUser/" + username;
            UserDatabaseAccess.AddLog(request, this.Request.Headers["ApiKey"]);
            
            if (UserDatabaseAccess.CheckUserApiKey(this.Request.Headers["ApiKey"])) 
            // Tried to get apikey using identity set up in authmiddleware.cs, couldnt access claims and there was an empty identity in there as well.
            {
                User user = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);
                if(user.UserName == username)
                {
                    UserDatabaseAccess.RemoveUser(user);
                    // User removed - Response.
                    return Ok(true);
                }
            }
            // User not removed - Response.
            return Ok(false);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("ChangeRole")]
        [HttpPost]
        public IActionResult PostChangeRole([FromBody] JSONContract JSONBodyResult)
        {
            // Action.
            User foundUser = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);

            // Logging. 
            string request = foundUser.UserName + " requested /User/RemoveRole/ with a body of: " + JSONBodyResult;
            UserDatabaseAccess.AddLog(request, this.Request.Headers["ApiKey"]);


            string result = UserDatabaseAccess.ChangeRole(JSONBodyResult);
            if (result.StartsWith("NOT"))
            {
                // Role not changed - Response.
                return BadRequest(result);
            }
            else
            {
                // Role change successful - Response.
                return Ok(result);
            }
        }
    }
}
