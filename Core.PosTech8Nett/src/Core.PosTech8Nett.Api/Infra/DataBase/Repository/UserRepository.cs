using Core.PosTech8Nett.Api.Domain.Entities.Identity;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Infra.DataBase.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UsersEntitie> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<UsersEntitie> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task CreateAsync(UsersEntitie entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);
            if (result.Succeeded is false)
            {
                //throw new Exception(result.Errors.);
                throw new Exception("Erro ao criar usuario");
            }

            //  await _userManager.AddToRoleAsync(entity, "user");

        }

        public async Task<bool> CheckPasswordAsync(UsersEntitie user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task DeleteAsync(UsersEntitie entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            if (result.Succeeded is false)
            {
                //throw new Exception(result.Errors.);
                throw new Exception("Erro ao criar usuario");
            }
        }

        public Task<IEnumerable<UsersEntitie>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<UsersEntitie> GetByEmailAsync(string email)
        {
            return await _context.Users
                                  .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsersEntitie> GetByIdAsync(object id)
        {
            return await _context.Users
                                  .FirstOrDefaultAsync(u => u.Id == Guid.Parse(id.ToString()));
        }

        public async Task<UsersEntitie> GetByNicknameAsync(string nickname)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NickName == nickname);
        }

        public Task SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(UsersEntitie entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task BlockUserAsync(UsersEntitie user, bool enableBlocking)
        {
            var result = await _userManager.SetLockoutEnabledAsync(user, enableBlocking);
            if (result.Succeeded is false)
            {
                //throw new Exception(result.Errors.);
                throw new Exception("Erro ao criar usuario");
            }
        }
    }
}
