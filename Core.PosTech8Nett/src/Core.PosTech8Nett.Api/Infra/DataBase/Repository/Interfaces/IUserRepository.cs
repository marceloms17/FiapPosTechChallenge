using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(UsersEntitie entity, string password);
        void Update(UsersEntitie entity);
        Task DeleteAsync(UsersEntitie entity);
        Task BlockUserAsync(UsersEntitie user, bool enableBlocking);
        Task<UsersEntitie> GetByIdAsync(object id);
        Task<UsersEntitie> GetByEmailAsync(string email);
        Task<UsersEntitie> GetByNicknameAsync(string nickname);
        Task<bool> CheckPasswordAsync(UsersEntitie user, string password);
        Task<IList<string>> GetRolesUser(UsersEntitie user);
        Task AccessFailedAsync(UsersEntitie user);
        Task<bool> CheckLockedOutAsync(UsersEntitie user);
    }
}
