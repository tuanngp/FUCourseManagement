using DataAccessObjects;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class UserDAO : BaseDAO<User, int>
    {
        protected override IQueryable<User> AddIncludes(IQueryable<User> query)
        {
            return query.Include(query => query.EnrollmentRecords).Include(query => query.Sessions);
        }
    }
}
