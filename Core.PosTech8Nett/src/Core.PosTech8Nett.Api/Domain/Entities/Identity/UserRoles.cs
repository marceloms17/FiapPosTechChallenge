using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class UserRoles  : IdentityUserRole<Guid>
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
    }
}
