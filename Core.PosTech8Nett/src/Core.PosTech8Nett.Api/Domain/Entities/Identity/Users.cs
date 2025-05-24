using Core.PosTech8Nett.Api.Domain.Entities.UserInformation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.PosTech8Nett.Api.Domain.Entities.Identity
{
    public class Users: IdentityUser<Guid>
    {
        public Users()
        {
            Id = Guid.NewGuid();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
}
