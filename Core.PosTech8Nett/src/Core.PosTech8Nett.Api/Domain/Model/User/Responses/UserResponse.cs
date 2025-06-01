using Core.PosTech8Nett.Api.Domain.Entities.UserInformation;
using System;

namespace Core.PosTech8Nett.Api.Domain.Model.User.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
