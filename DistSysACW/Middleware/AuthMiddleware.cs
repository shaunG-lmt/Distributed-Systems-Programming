using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DistSysACW.Models;

namespace DistSysACW.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Models.UserContext dbContext)
        {
            #region Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            //        Then set the correct roles for the User, using claims
            #endregion

            var header = context.Request.Headers;
            if (header.ContainsKey("ApiKey"))
            {
                string apikey = header["ApiKey"];

                User founduser = UserDatabaseAccess.checkApiKeyReturnUser(apikey);

                var identity = new ClaimsIdentity(new[]
                {

                    new Claim(ClaimTypes.AuthenticationMethod, "ApiKey"),
                    new Claim(ClaimTypes.Name, founduser.UserName),
                    new Claim(ClaimTypes.Role, founduser.Role)
                });

                context.User.AddIdentity(identity);

            }
            else
            {
                // No apikey, do nothing.
            }


            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

    }
}