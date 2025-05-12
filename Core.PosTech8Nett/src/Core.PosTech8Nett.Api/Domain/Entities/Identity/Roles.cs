using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class Roles : IdentityRole<Guid>
    {
        public Roles()
        {

            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
