using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistSysACW.Models;

namespace DistSysACW.Filters
{
    public class AuthFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AuthorizeAttribute authAttribute = (AuthorizeAttribute)context.ActionDescriptor.EndpointMetadata.Where(e => e.GetType() == typeof(AuthorizeAttribute)).FirstOrDefault();

            if (authAttribute != null)
            {
                // Invalid Apikey
                if (UserDatabaseAccess.CheckUserApiKey(context.HttpContext.Request.Headers["ApiKey"]) == false)
                {
                    // Invalid Apikey for request with admin role.
                    if (authAttribute.Roles == "Admin")
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult("Unauthorized. Admin access only.");
                        return;
                    }
                    context.Result = new JsonResult("Unauthorized. Check ApiKey in Header is correct.");
                    context.HttpContext.Response.StatusCode = 401;
                    return;
                }
                else
                {
                    string[] roles = authAttribute.Roles.Split(',');
                    foreach(string role in roles)
                    {
                        string jeff = role.Trim();
                        if (context.HttpContext.User.IsInRole(jeff)) 
                        {
                            return;
                        }
                    }
                    // User does not have admin role.
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult("Unauthorized. Admin access only.");

                }
            }
        }
    }
}
