using CA.ERP.Domain.UserAgg;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetCurrentUserId()
        {
            var userId = Guid.Empty;
            if (_httpContextAccessor.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                Claim claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                Guid.TryParse(claim.Value.ToString(), out userId);
            }

            return userId;
        }

    }
}
