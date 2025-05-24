using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Enum;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.UserInformation
{
    public class Contact
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ContactTypeEnum IdContactType { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public UsersEntitie User { get; set; }
    }
}
