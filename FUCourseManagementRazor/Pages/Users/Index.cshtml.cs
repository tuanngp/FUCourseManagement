using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FUBusiness.Models;

namespace FUCourseManagementRazor.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly FUBusiness.Models.CourseEnrollManagementContext _context;

        public IndexModel(FUBusiness.Models.CourseEnrollManagementContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
