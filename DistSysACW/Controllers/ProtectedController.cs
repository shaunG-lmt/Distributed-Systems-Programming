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

        //[Authorize(Roles = "Admin, User")]
        //[ActionName("SHA1")]
        //[HttpGet]
        //public ActionResult Sha1()
        //{
        //    string output = "a";
        //    return Ok(output);
        //}

        //[Authorize(Roles = "Admin, User")]
        //[ActionName("SHA256")]
        //[HttpGet]
        //public ActionResult Sha256()
        //{
        //    string output = "a";
        //    return Ok(output);
        //}
    }
}