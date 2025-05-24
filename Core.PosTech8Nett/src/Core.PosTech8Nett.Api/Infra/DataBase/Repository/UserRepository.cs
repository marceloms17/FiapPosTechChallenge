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
    public class UserRepository : Repository<UsersEntitie>, IUserRepository
    {
        private readonly UserManager<UsersEntitie> _userManager;
        public UserRepository(UserManager<UsersEntitie> userManager, ApplicationDbContext context) : base(context)
        {
            _userManager = userManager;
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

        public async Task AddAsync(UsersEntitie entity, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckPasswordAsync(UsersEntitie user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public void Delete(UsersEntitie entity)
        {
            throw new System.NotImplementedException();
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

        public Task<UsersEntitie> GetByIdAsync(object id)
        {
            throw new System.NotImplementedException();
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
    }
}
