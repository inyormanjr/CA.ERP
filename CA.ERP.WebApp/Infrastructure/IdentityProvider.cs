using CA.ERP.Domain.IdentityAgg;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Infrastructure
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Identity> GetCurrentIdentity()
        {
            var id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var guid = !string.IsNullOrEmpty(id) ? Guid.Parse(id) : Guid.Empty;

            var roles = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "role").Select(c => c.Value);
            var branches = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "branch").Select(c => Guid.Parse(c.Value));
            var result = Identity.Create(guid, branches, roles);
            return Task.FromResult(result.IsSuccess ? result.Result : null);
        }
    }
}
