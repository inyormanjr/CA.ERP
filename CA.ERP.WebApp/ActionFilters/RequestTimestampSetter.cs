using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.ActionFilters
{
    /// <summary>
    /// Copy's the header startTimestamp from request to response if it exist 
    /// </summary>
    public class RequestTimestampSetter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            if (context.HttpContext.Request.Headers.TryGetValue("startTimestamp", out StringValues timestamp))
            {
                context.HttpContext.Response.Headers.Add("startTimestamp", timestamp);
            }
        }
    }
}
