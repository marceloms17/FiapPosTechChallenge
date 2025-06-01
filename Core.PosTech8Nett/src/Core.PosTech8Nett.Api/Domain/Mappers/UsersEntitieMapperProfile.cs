using AutoMapper;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using System;

namespace Core.PosTech8Nett.Api.Domain.Mappers
{
    public class UsersEntitieMapperProfile : Profile
    {
        public UsersEntitieMapperProfile()
        {
            CreateMap<UsersEntitie, UserResponse>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.UserName))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.LockoutEnd, options => options.MapFrom(source => source.LockoutEnd))
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
                .ForMember(destination => destination.Birthdate, options => options.MapFrom(source => source.Birthdate))
                .ForMember(destination => destination.NickName, options => options.MapFrom(source => source.NickName))
                .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => source.CreatedAt))
                .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => source.CreatedAt))
                .ForMember(destination => destination.UpdateAt, options => options.MapFrom(source => source.UpdateAt));


            CreateMap<CreateUserRequest, UsersEntitie>()
                    .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
                    .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                    .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
                    .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
                    .ForMember(destination => destination.Birthdate, options => options.MapFrom(source => source.Birthdate))
                    .ForMember(destination => destination.NickName, options => options.MapFrom(source => source.NickName))
                    .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => DateTime.Now))
                    .ForMember(destination => destination.UpdateAt, options => options.MapFrom(source => DateTime.Now));


        }
    }
}
