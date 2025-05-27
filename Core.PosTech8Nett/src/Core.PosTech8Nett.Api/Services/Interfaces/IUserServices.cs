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
        Task<UserResponse> GetByNickNameAsync(GetUserByNickNameRequest request);
        Task<UserResponse> GetByIdAsync(GetUserByIdRequest request);
        Task<UserResponse> UpdateAsync(CreateUserRequest request);
        Task DeleteAsync(DeleteUserRequest request);
        Task BlockUserAsync(BlockUserRequest request);

    }
}
