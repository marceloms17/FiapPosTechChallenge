using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class UserRoles  : IdentityUserRole<Guid>
    {
        public UsersEntitie User { get; set; }
        public Roles Role { get; set; }
    }
}
