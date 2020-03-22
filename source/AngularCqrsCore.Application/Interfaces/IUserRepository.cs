using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByLoginAsync(string login);
        Task<User> GetAsync(int id);
        Task<List<User>> GetAllAsync();
        void DeleteUser(User user);
    }
}
