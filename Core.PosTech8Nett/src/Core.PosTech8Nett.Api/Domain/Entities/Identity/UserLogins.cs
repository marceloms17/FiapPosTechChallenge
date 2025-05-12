using Microsoft.AspNetCore.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class UserLogins: IdentityUserLogin<Guid>
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public Guid UserId { get; set; }
    }
}
