using CA.ERP.WebApp.EventSources;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.ActionFilters
{
    public class RequestProcessingTimeFilter : IAsyncActionFilter
    {
        readonly Stopwatch _stopwatch = new Stopwatch();

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _stopwatch.Start();
            await next();
            _stopwatch.Stop();
            RequestProcessingSource.Log.Request( context.HttpContext.Request.GetDisplayUrl(), _stopwatch.ElapsedMilliseconds);
        }
    }
}
