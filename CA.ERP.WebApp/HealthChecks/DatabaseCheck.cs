using CA.ERP.DataAccess;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.HealthChecks
{
    public class DatabaseCheck : IHealthCheck
    {
        private readonly CADataContext _context;

        public DatabaseCheck(CADataContext context)
        {
            _context = context;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            HealthCheckResult ret;
            if (await _context.Database.CanConnectAsync())
            {
                ret = HealthCheckResult.Healthy("Healthy");
            }
            else
            {
                ret = HealthCheckResult.Unhealthy("Unhealthy");
            }
            return ret;
        }
    }
}
