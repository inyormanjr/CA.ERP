using CA.ERP.WebApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.CustomAuthentication
{
    /// <summary>
    /// A custom principal for modified behavior.
    /// </summary>
    public class CustomClaimsPrincipal : ClaimsPrincipal
    {
        ///<inheritdoc></inheritdoc>/>
        public CustomClaimsPrincipal(IPrincipal principal):base(principal)
        {

        }

        /// <inheritdoc/>
        public override bool IsInRole(string role)
        {
            var isInRole = base.IsInRole(role);
            if (!isInRole)
            {
                //check if required role is parseble
                if (Enum.TryParse<UserRole>(role, out UserRole requiredUserRole))
                {

                    //do custom role check
                    //get role int
                    var roleIntClaim = Claims.FirstOrDefault(c => c.Type == "RoleInt");
                    if (roleIntClaim != null)
                    {
                        //convert to enum
                        if (int.TryParse(roleIntClaim.Value, out int roleInt))
                        {
                            UserRole userRole = (UserRole)roleInt;
                            isInRole = userRole.HasFlag(requiredUserRole);
                        }

                    }
                }

            }
            return isInRole;
        }
    }
}
