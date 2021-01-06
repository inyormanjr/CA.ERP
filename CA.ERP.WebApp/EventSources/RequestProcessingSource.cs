using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.EventSources
{
    [EventSource(Name = "CA.ERP.Metrics.Request.Processing.Time")]
    public sealed class RequestProcessingSource: EventSource
    {
        public static readonly RequestProcessingSource Log = new RequestProcessingSource();
        private EventCounter _requestCounter;

        private RequestProcessingSource() =>
            _requestCounter = new EventCounter("request-time", this)
            {
                DisplayName = "Request Processing Time",
                DisplayUnits = "ms"
            };

        public void Request(string url, float elapsedMilliseconds)
        {
            WriteEvent(1, url, elapsedMilliseconds);
            _requestCounter?.WriteMetric(elapsedMilliseconds);
        }

        protected override void Dispose(bool disposing)
        {
            _requestCounter?.Dispose();
            _requestCounter = null;

            base.Dispose(disposing);
        }

    }
}
