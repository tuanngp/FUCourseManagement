using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUBusiness.Models;

namespace Repositories
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<User> Login(string email, string password);
    }
}
