﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.Repositories;

namespace Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AngularCoreContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetByLoginAsync(string login) => await Entities.Include(u => u.Photos).Where(e => e.Login.ToLower().Trim() == login.ToLower().Trim()).SingleOrDefaultAsync();
        public async Task<User> GetAsync(int id) => await Entities.Include(e => e.Photos).Where(e => e.Id == id).SingleOrDefaultAsync();
        public async Task<List<User>> GetAllAsync() => await Entities.Include(e => e.Photos).ToListAsync();

        public void DeleteUser(User user) => Delete(user);
    }
}
