﻿using Core.PosTech8Nett.Api.CommonExtensions;
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
                var messages = string.Concat("Message is invalid, validation errors: ", result.Errors.ConvertToString());
                throw new Exception(messages);
            }

            var role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Usuario"));

            _context.UserRoles.Add(new UserRoles()
            {
                UserId = entity.Id,
                RoleId = role.Id
            });

            _context.SaveChanges();
        }

        public async Task<bool> CheckPasswordAsync(UsersEntitie user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task AccessFailedAsync(UsersEntitie user)
        {
            var result = await _userManager.AccessFailedAsync(user);
            if (result.Succeeded is false)
            {
                var messages = string.Concat("Message is invalid, validation errors: ", result.Errors.ConvertToString());
                throw new Exception(messages);
            }
        }

        public async Task<bool> CheckLockedOutAsync(UsersEntitie user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }

        public async Task DeleteAsync(UsersEntitie entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            if (result.Succeeded is false)
            {
                var messages = string.Concat("Message is invalid, validation errors: ", result.Errors.ConvertToString());
                throw new Exception(messages);
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

        public void Update(UsersEntitie entity)
        {
            _context.Update(entity);
            _context.SaveChangesAsync();
        }

        public async Task BlockUserAsync(UsersEntitie user, bool enableBlocking)
        {
            if (enableBlocking)
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddMinutes(10));
                if (result.Succeeded is false)
                {
                    var messages = string.Concat("Message is invalid, validation errors: ", result.Errors.ConvertToString());
                    throw new Exception(messages);
                }
            }
            else
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
                if (result.Succeeded is false)
                {
                    var messages = string.Concat("Message is invalid, validation errors: ", result.Errors.ConvertToString());
                    throw new Exception(messages);
                }
            }
        }

        public async Task<IList<string>> GetRolesUser(UsersEntitie user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}