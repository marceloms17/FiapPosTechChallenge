using AutoMapper;
using Core.PosTech8Nett.Api.CommonExtensions;
using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Core.PosTech8Nett.Api.Domain.Validations.User;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Core.PosTech8Nett.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<bool> BlockUserAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(CreateUserRequest request)
        {
            var persistence = _mapper.Map<UsersEntitie>(request);
            await _userRepository.CreateAsync(persistence, request.Password);
        }

        public Task<bool> DeleteAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> GetByEmailAsync(GetUserByEmailRequest request)
        {
            var resultValidate = new GetUserByEmailRequestValidator().Validate(request);

            if (resultValidate.IsValid is false)
            {
                var messages = string.Concat("Message is invalid, validation errors: ", resultValidate.Errors.ConvertToString());
                throw new Exception(messages);
            }

           var data = await _userRepository.GetByEmailAsync(request.Email);

           return _mapper.Map<UserResponse>(data);
        }

        public Task<UserResponse> GetByIdAsync(GetUserByEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetByNickNameAsync(GetUserByEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> UpdateAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
