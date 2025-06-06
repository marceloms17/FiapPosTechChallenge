﻿using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using System;

namespace Core.PosTech8Nett.Api.Domain.Entities.UserInformation
{
    public class Address
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public UsersEntitie User { get; set; }
    }
}
