using FUBusiness.Models;
using FUDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Repositories.RepositoriesImpl
{
    public class UserRepository : IUserRepository
    {
        private UserDAO _dao;

        public UserRepository()
        {
            _dao = new UserDAO();
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _dao.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<User> Login(string email, string password)
        {
            return await _dao.GetQueryable()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            return await _dao.UpdateAsync(entity);
        }
    }
}
