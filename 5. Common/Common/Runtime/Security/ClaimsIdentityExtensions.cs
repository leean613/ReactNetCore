using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Common.Runtime.Security
{
    public static class ClaimsIdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if(identity == null)
            {
                throw new ArgumentNullException();
            }    

            var userIdClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                throw new AuthenticationException();
            }

            Guid userId;
            if (!Guid.TryParse(userIdClaim.Value, out userId))
            {
                throw new AuthenticationException();
            }

            return userId;
        }
    }
}
