using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FUBusiness.Models;

namespace FUCourseManagement.Controllers
{
    public class EnrollmentRecordsController : Controller
    {
        private readonly CourseEnrollManagementContext _context;

        public EnrollmentRecordsController(CourseEnrollManagementContext context)
        {
            _context = context;
        }

        // GET: EnrollmentRecords
        public async Task<IActionResult> Index()
        {
            var courseEnrollManagementContext = _context.EnrollmentRecords.Include(e => e.User);
            return View(await courseEnrollManagementContext.ToListAsync());
        }

        // GET: EnrollmentRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentRecord = await _context.EnrollmentRecords
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollmentRecord == null)
            {
                return NotFound();
            }

            return View(enrollmentRecord);
        }

        // GET: EnrollmentRecords/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: EnrollmentRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CourseId,EnrollDate,Dropped")] EnrollmentRecord enrollmentRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollmentRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", enrollmentRecord.UserId);
            return View(enrollmentRecord);
        }

        // GET: EnrollmentRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentRecord = await _context.EnrollmentRecords.FindAsync(id);
            if (enrollmentRecord == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", enrollmentRecord.UserId);
            return View(enrollmentRecord);
        }

        // POST: EnrollmentRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CourseId,EnrollDate,Dropped")] EnrollmentRecord enrollmentRecord)
        {
            if (id != enrollmentRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollmentRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentRecordExists(enrollmentRecord.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", enrollmentRecord.UserId);
            return View(enrollmentRecord);
        }

        // GET: EnrollmentRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentRecord = await _context.EnrollmentRecords
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollmentRecord == null)
            {
                return NotFound();
            }

            return View(enrollmentRecord);
        }

        // POST: EnrollmentRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollmentRecord = await _context.EnrollmentRecords.FindAsync(id);
            if (enrollmentRecord != null)
            {
                _context.EnrollmentRecords.Remove(enrollmentRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentRecordExists(int id)
        {
            return _context.EnrollmentRecords.Any(e => e.Id == id);
        }
    }
}
