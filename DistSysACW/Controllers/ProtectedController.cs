using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DistSysACW.Models;

namespace DistSysACW.Controllers
{
    public class ProtectedController : BaseController
    {
        public ProtectedController(Models.UserContext context) : base(context) { }


        [Authorize(Roles = "Admin, User")]
        [ActionName("Hello")]
        [HttpGet]
        public IActionResult Get()
        {
            User foundUser = UserDatabaseAccess.ReturnUserFromApiKey(this.Request.Headers["ApiKey"]);
            return Ok("Hello "+ foundUser.UserName);
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("Sha1")]
        [HttpGet]
        public ActionResult Sha1([FromQuery] string message)
        {
            if (message == null)
            {
                return BadRequest("Bad Request");
            }
            else
            {
                return Ok(Hashing.SHA1(message));
            }
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("Sha256")]
        [HttpGet]
        public ActionResult Sha256([FromQuery] string message)
        {
            if (message == null)
            {
                return BadRequest("Bad Request");
            }
            else
            {
                return Ok(Hashing.SHA256(message));
            }
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("GetPublicKey")]
        [HttpGet]
        public ActionResult SendPublicKey()
        {
            return Ok(Crypto.Instance.GetPublic());
        }

        [Authorize(Roles = "Admin, User")]
        [ActionName("Sign")]
        [HttpGet]
        public ActionResult Signed([FromQuery] string message)
        {
            return Ok(Crypto.Instance.SignMessage(message));
        }
    }
}