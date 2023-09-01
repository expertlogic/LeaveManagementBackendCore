using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Extensions
{
    public static class HttpContextExtension
    {
        public static bool IsAuthenticated(this HttpContext httpContext)
        {
            return httpContext != null
                && httpContext.User != null
                && httpContext.User.Identity != null
                && httpContext.User.Identity.IsAuthenticated;
        }

        public static string GetClaimValue(this HttpContext httpContext, string claimType)
        {
            if(httpContext.IsAuthenticated()
                && httpContext.User.Claims.Count() > 0)
            {
                return httpContext.User.Claims.Where(c => c.Type.ToLower().Equals(claimType.ToLower())).FirstOrDefault()?.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
