using System;

namespace Core.PosTech8Nett.Api.Domain.Model.User.Requests
{
    public class CreateUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string NickName { get; set; }
    }
}