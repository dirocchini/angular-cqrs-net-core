using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<User> GetByLogin(string login) => Entities.Where(e => e.Login.ToLower().Trim() == login.ToLower().Trim()).SingleOrDefaultAsync();
    }
}
