using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class Roles : IdentityRole<Guid>
    {
        public Roles()
        {

            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public List<UserRoles> UserRoles { get; set; }
    }
}
