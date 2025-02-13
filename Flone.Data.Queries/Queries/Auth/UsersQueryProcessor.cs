﻿using Flone.Api.Common.Exceptions;
using Flone.Api.Models.Users;
using Flone.Data.Models;
using Flone.Data.Repository.DAL;
using Flone.Data.Repository.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Queries.Queries.Auth
{
    public class UsersQueryProcessor : IUsersQueryProcessor
    {
        private readonly IUnitOfWork _uow;

        public UsersQueryProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IQueryable<User> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<User> GetQuery()
        {
            return _uow.Query<User>()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role);
        }

        public User Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        public async Task<User> Create(CreateUserModel model)
        {
            var username = model.UserName.Trim();

            if (GetQuery().Any(u => u.Username == username))
            {
                throw new BadRequestException("The username is already in use");
            }

            var user = new User
            {
                Username = model.UserName.Trim(),
                Password = model.Password.Trim().WithBCrypt(),
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
            };

            AddUserRoles(user, model.Roles);

            _uow.Add(user);
            await _uow.CommitAsync();

            return user;
        }

        private void AddUserRoles(User user, string[] roleNames)
        {
            user.Roles.Clear();

            foreach (var roleName in roleNames)
            {
                var role = _uow.Query<Role>().FirstOrDefault(x => x.Name == roleName);

                if (role == null)
                {
                    throw new NotFoundException($"Role - {roleName} is not found");
                }

                user.Roles.Add(new UserRole { User = user, Role = role });
            }
        }

        public async Task<User> Update(int id, UpdateUserModel model)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.Username = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            AddUserRoles(user, model.Roles);

            await _uow.CommitAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _uow.CommitAsync();
        }

        public async Task ChangePassword(int id, ChangeUserPasswordModel model)
        {
            var user = Get(id);
            user.Password = model.Password.WithBCrypt();
            await _uow.CommitAsync();
        }
    }
}
