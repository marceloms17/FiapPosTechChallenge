using Core.PosTech8Nett.Api.Domain.Entities.UserInformation;
using System;

namespace Core.PosTech8Nett.Api.Domain.Model.User.Requests
{
    public class UpdateUserRequest
    {
        public int UserName { get; set; }
        public int Email { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}
