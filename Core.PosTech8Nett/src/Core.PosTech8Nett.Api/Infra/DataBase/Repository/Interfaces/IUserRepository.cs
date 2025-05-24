using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces
{
    public interface IUserRepository: IRepository<UsersEntitie>
    {
        Task CreateAsync(UsersEntitie entity, string password);
        Task<UsersEntitie> GetByEmailAsync(string email);
        Task<UsersEntitie> GetByNicknameAsync(string nickname);
        Task<bool> CheckPasswordAsync(UsersEntitie user, string password);
      
    }
}
