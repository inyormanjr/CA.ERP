using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Authentication
{
    public class RefreshTokenResponse
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
