using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Payzi.Enumerate
{
    public static class IdentityClaims
    {
        private const string EmailClaimType = "Email";

        public static void AddEmailClaim(this ClaimsIdentity identity, string email)
        {
            identity.AddClaim(new Claim(EmailClaimType, email));
        }

        public static string GetEmailClaim(this ClaimsIdentity identity)
        {
            var claim = identity.FindFirst(EmailClaimType);
            return claim?.Value;
        }
    }
}
