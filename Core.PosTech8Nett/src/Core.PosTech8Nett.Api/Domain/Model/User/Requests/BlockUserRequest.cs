using System;

namespace Core.PosTech8Nett.Api.Domain.Model.User.Requests
{
    public class BlockUserRequest
    {
        public Guid Id { get; set; }
        public bool EnableBlocking { get; set; }
    }
}
