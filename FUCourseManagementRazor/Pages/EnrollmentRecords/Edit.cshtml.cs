using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FUBusiness.Models;

namespace FUCourseManagementRazor.Pages.EnrollmentRecords
{
    public class EditModel : PageModel
    {
        private readonly FUBusiness.Models.CourseEnrollManagementContext _context;

        public EditModel(FUBusiness.Models.CourseEnrollManagementContext context)
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

            var enrollmentrecord =  await _context.EnrollmentRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (enrollmentrecord == null)
            {
                return NotFound();
            }
            EnrollmentRecord = enrollmentrecord;
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EnrollmentRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentRecordExists(EnrollmentRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EnrollmentRecordExists(int id)
        {
            return _context.EnrollmentRecords.Any(e => e.Id == id);
        }
    }
}
