using DataAccessObjects;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class SessionDAO : BaseDAO<Session, string>
    {
        protected override IQueryable<Session> AddIncludes(IQueryable<Session> query)
        {
            return query.Include(query => query.User);
        }
    }
}
