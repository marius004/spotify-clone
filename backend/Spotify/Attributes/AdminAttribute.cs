using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using Spotify.Entities;

namespace Spotify.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User) context.HttpContext.Items["User"];
            
            Console.WriteLine(user.ToJson());
            
            if (user == null || user.IsAdmin == false)
                context.Result = new JsonResult(new {message = "Unauthorized"})
                    {StatusCode = StatusCodes.Status401Unauthorized};
        }
    }
}