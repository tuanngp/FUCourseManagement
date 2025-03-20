using FUBusiness.Models;
using FUDataAccess;

namespace Repositories.RepositoriesImpl
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDAO _courseDAO;

        public CourseRepository()
        {
            _courseDAO = new CourseDAO();
        }

        public async Task<Course> AddAsync(Course entity)
        {
            return await _courseDAO.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _courseDAO.DeleteAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _courseDAO.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseDAO.GetByIdAsync(id);
        }

        public async Task<Course> UpdateAsync(Course entity)
        {
            return await _courseDAO.UpdateAsync(entity);
        }
    }
}
