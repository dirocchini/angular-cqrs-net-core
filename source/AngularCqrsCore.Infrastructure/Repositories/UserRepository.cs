using System.Collections.Generic;
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

        public async Task<User> GetByLoginAsync(string login) => await Entities.Where(e => e.Login.ToLower().Trim() == login.ToLower().Trim()).SingleOrDefaultAsync();
        public async Task<List<User>> GetAllAsync() => await Entities.ToListAsync();
    }
}
