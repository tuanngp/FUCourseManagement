using FUBusiness.Models;
using FUDataAccess;

namespace Repositories.RepositoriesImpl
{
    public class EnrollmentRecordRepository : IEnrollmentRecordRepository
    {
        private EnrollmentRecordDAO _dao;

        public EnrollmentRecordRepository()
        {
            _dao = new EnrollmentRecordDAO();
        }

        public async Task<EnrollmentRecord> AddAsync(EnrollmentRecord entity)
        {
            return await _dao.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<IEnumerable<EnrollmentRecord>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<EnrollmentRecord?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<EnrollmentRecord> UpdateAsync(EnrollmentRecord entity)
        {
            return await _dao.UpdateAsync(entity);
        }
    }
}
