using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Portolo.Framework.Security
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        public const string IssuerName = "http://www.Portolo.com/2014/07/issuer";
        private readonly Guid id;

        public AuthorizationPolicy()
        {
            this.id = Guid.NewGuid();
            var claim = Claim.CreateNameClaim(IssuerName);

            var claims = new Claim[1];
            claims[0] = claim;
            this.Issuer = new DefaultClaimSet(claims);
        }

        public ClaimItems Claims { get; set; }

        public string Id => this.id.ToString();

        public ClaimSet Issuer { get; }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            var context = HttpContext.Current;
            var claimSet = this.GetClaimSetByIdentity(context.User.Identity);
            if (claimSet != null)
            {
                evaluationContext.AddClaimSet(this, claimSet);
            }

            return true;
        }

        protected virtual ClaimSet GetClaimSetByIdentity(IIdentity identity)
        {
            var claims = new List<Claim>();

            if (this.Claims.Any())
            {
                foreach (var ci in this.Claims)
                {
                    claims.Add(new Claim(ci.ClaimType, ci.Resource, Rights.PossessProperty));
                }
            }

            return new DefaultClaimSet(this.Issuer, claims);
        }
    }
}