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
                if (UserDatabaseAccess.checkUserApiKey(context.HttpContext.Request.Headers["ApiKey"]))
                {
                    string[] roles = authAttribute.Roles.Split(',');
                    if (context.HttpContext.User.IsInRole(roles[0]))
                    {
                        // User has role "Admin"
                        return;
                    }
                    try
                    {
                        if (context.HttpContext.User.IsInRole(roles[1]))
                        {
                            // User has role "User"
                            return;
                        }
                    }
                    catch
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult("Unauthorized. Admin access only.");
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult("Unauthorized. Check ApiKey in Header is correct.");

                }
                    

            }
        }
    }
}
