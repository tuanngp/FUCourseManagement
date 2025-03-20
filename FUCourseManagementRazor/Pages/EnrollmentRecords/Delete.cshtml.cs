using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FUBusiness.Models;

namespace FUCourseManagementRazor.Pages.EnrollmentRecords
{
    public class DeleteModel : PageModel
    {
        private readonly FUBusiness.Models.CourseEnrollManagementContext _context;

        public DeleteModel(FUBusiness.Models.CourseEnrollManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EnrollmentRecord EnrollmentRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentrecord = await _context.EnrollmentRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (enrollmentrecord == null)
            {
                return NotFound();
            }
            else
            {
                EnrollmentRecord = enrollmentrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentrecord = await _context.EnrollmentRecords.FindAsync(id);
            if (enrollmentrecord != null)
            {
                EnrollmentRecord = enrollmentrecord;
                _context.EnrollmentRecords.Remove(EnrollmentRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
