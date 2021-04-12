using ApplicationCore.DatabaseContext;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserServices : IUser
    {
        private readonly EProcurementContext _procContext;

        public UserServices(EProcurementContext procContext)
        {
            _procContext = procContext;
        }

        public async Task<List<User>> GetUser()
        {
            return await _procContext.User.OrderBy(o => o.FirstName).ToListAsync();
        }

        public async Task<User> GetUser(string userId)
        {
            //return await _procContext.User.FirstOrDefaultAsync(y => y.UserId == userId);

            // HARDCODE
            return await _procContext.User.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            //return await _procContext.User.FirstOrDefaultAsync(y => y.Username.ToLower() == username.ToLower());

            // HARDCODE
            return await _procContext.User.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByLogin(string username, string password)
        {
            //return await _procContext.User
            //    .FirstOrDefaultAsync(y => y.Username.ToLower() == username.ToLower() && y.Password == password);


            // HARDCODE
            return await _procContext.User.FirstOrDefaultAsync();
        }
    }
}
