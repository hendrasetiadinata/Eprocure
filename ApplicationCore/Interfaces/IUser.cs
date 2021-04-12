using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUser
    {
        Task<List<User>> GetUser();
        Task<User> GetUser(string userId);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByLogin(string username, string password);
    }
}
