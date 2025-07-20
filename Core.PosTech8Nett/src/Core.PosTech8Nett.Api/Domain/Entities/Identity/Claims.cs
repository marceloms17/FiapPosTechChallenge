using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class Claims : IdentityRoleClaim<Guid>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
