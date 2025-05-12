using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class UserClaims: IdentityUserClaim<Guid>
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
