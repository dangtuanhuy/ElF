using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkAptech.Data;

namespace WorkAptech.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/EventJobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventJob.Include(e => e.Category).Include(e => e.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/EventJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob
                .Include(e => e.Category)
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJob == null)
            {
                return NotFound();
            }

            return View(eventJob);
        }

        // GET: Admin/EventJobs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address");
            return View();
        }

        // POST: Admin/EventJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TiTle,FromDate,ToDate,Hour,EventStatus,Image,CategoryId,CompanyId")] EventJob eventJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return View(eventJob);
        }

        // GET: Admin/EventJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob.FindAsync(id);
            if (eventJob == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return View(eventJob);
        }

        // POST: Admin/EventJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TiTle,FromDate,ToDate,Hour,EventStatus,Image,CategoryId,CompanyId")] EventJob eventJob)
        {
            if (id != eventJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventJobExists(eventJob.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", eventJob.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Address", eventJob.CompanyId);
            return View(eventJob);
        }

        // GET: Admin/EventJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJob = await _context.EventJob
                .Include(e => e.Category)
                .Include(e => e.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJob == null)
            {
                return NotFound();
            }

            return View(eventJob);
        }

        // POST: Admin/EventJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventJob = await _context.EventJob.FindAsync(id);
            _context.EventJob.Remove(eventJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventJobExists(int id)
        {
            return _context.EventJob.Any(e => e.Id == id);
        }
    }
}
