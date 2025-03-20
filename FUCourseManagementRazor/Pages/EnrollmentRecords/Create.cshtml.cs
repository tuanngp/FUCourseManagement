using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FUBusiness.Models;

namespace FUCourseManagementRazor.Pages.EnrollmentRecords
{
    public class CreateModel : PageModel
    {
        private readonly FUBusiness.Models.CourseEnrollManagementContext _context;

        public CreateModel(FUBusiness.Models.CourseEnrollManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public EnrollmentRecord EnrollmentRecord { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EnrollmentRecords.Add(EnrollmentRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
