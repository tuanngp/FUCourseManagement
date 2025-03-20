using FUBusiness.Models;
using FUDataAccess;

namespace Repositories.RepositoriesImpl
{
    public class SessionRepository : ISessionRepository
    {
        private SessionDAO _dao;

        public SessionRepository()
        {
            _dao = new SessionDAO();
        }

        public async Task<Session> AddAsync(Session entity)
        {
            entity.SessionId = Guid.NewGuid().ToString();
            return await _dao.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Session?> GetByIdAsync(String id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<Session> UpdateAsync(Session entity)
        {
            return await _dao.UpdateAsync(entity);
        }
    }
}
