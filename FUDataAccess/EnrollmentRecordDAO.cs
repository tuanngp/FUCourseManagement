using DataAccessObjects;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class EnrollmentRecordDAO : BaseDAO<EnrollmentRecord, int>
    {
        protected override IQueryable<EnrollmentRecord> AddIncludes(
            IQueryable<EnrollmentRecord> query
        )
        {
            return query.Include(query => query.User).Include(query => query.Course);
        }
    }
}
