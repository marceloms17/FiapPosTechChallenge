using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using Core.PosTech8Nett.Api.Domain.Model.User.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Services.Interfaces
{
    public interface IUserServices
    {
        Task CreateAsync(CreateUserRequest request);
        Task<UserResponse> GetByEmailAsync(GetUserByEmailRequest request);
        Task<UserResponse> GetByNickNameAsync(GetUserByEmailRequest request);
        Task<UserResponse> GetByIdAsync(GetUserByEmailRequest request);
        Task<UserResponse> UpdateAsync(CreateUserRequest request);

        Task<bool> DeleteAsync(CreateUserRequest request);

        Task<bool> BlockUserAsync(CreateUserRequest request);

    }
}
