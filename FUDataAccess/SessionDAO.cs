using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObjects;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class SessionDAO : BaseDAO<Session, int>
    {
        protected override IQueryable<Session> AddIncludes(IQueryable<Session> query)
        {
            return query.Include(query => query.User);
        }
    }
}
